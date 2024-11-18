/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       11/04/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using IPInfo.Entities.Model;
using IPInfo.Entities.Repository;
using IPInfo.Entities.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace IPInfo.ApiControllers
{
    public class IP2Country
    {
        private readonly IIPAddressRepository _ipAddressRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IIP2CountryService _ip2CountryService;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;

        public IP2Country(ILogger logger, IMemoryCache cache, IIPAddressRepository ipAddressRepository, ICountryRepository countryRepository, IIP2CountryService ip2CountryService)
        {
            _logger = logger;
            _cache = cache;
            _ipAddressRepository = ipAddressRepository;
            _countryRepository = countryRepository;
            _ip2CountryService = ip2CountryService; 
        }

        public async Task<ActionResult<Country>> GetIPCountry(string ip)
        {
            IPAddress ipAddress = null;
            Country country = null;

            // check is valid ip address
            if (!Helper.CheckIsValidIPAddress(ip))
            {
                string message = $"IP Address ({ip}) inputed is not valid.";
                _logger.LogError(message);
                return new BadRequestObjectResult(message);
            }

            // search ip address in cache
            if (_cache.TryGetValue(ip, out country))
            {
                _logger.LogInformation($"Country {country} found in cache for IP {ip}.");
            }
            else
            {
                // ip address not found in cache, looking for it in database
                _logger.LogInformation("IP Address not found in cache. Loading from database.");
                ipAddress = await _ipAddressRepository.FindIPAddressByIPAsync(ip);

                if (ipAddress != null)
                {
                    _logger.LogInformation($"IP Address {ipAddress} found in database.");

                    // search country on database
                    country = await _countryRepository.FindCountryByIDAsync(ipAddress.CountryId);
                }
                else
                {
                    // ip address not found in database, looking for a country in IP2C
                    _logger.LogInformation("IP Address not found in database. Loading from IP2C.");
                    country = await _ip2CountryService.GetCountryAsync(ip);

                    if (country == null)
                    {
                        _logger.LogError($"No country found for ip {ip} in IP2C.");
                    }
                    else
                    {
                        _logger.LogInformation($"Country {country} found in IP2C for ip {ip}.");

                        ipAddress = new IPAddress()
                        {
                            IP = ip,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        };

                        // add country if not exists in database
                        var dbCountry = await _countryRepository.FindCountryByNameAsync(country.Name);
                        if (dbCountry == null)
                        {
                            // save IP2C country to database
                            _countryRepository.AddCountry(country);

                            ipAddress.CountryId = country.Id; // new country ID
                        }
                        else
                        {
                            ipAddress.CountryId = dbCountry.Id; // persisted country ID
                        }

                        _ipAddressRepository.AddIpAddress(ipAddress);
                    }
                }

                // Save Country to cache
                if (country != null)
                {
                    _cache.Set(ip, country);
                }
                else
                {
                    string message = $"Country not found for IP {ip}.";
                    _logger.LogError(message);
                    return new NotFoundObjectResult(message);
                }
            }

            return new OkObjectResult(country);
        }
    }
}
