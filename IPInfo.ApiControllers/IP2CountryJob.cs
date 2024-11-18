/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       11/04/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using IPInfo.Entities.Repository;
using IPInfo.Entities.Service;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPInfo.ApiControllers
{
    public class IP2CountryJob
    {
        private readonly ILogger _logger;
        private readonly IIPAddressRepository _ipAddressRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IIP2CountryService _ip2CountryService;
        private readonly IMemoryCache _cache;

        public IP2CountryJob(ILogger logger, IIPAddressRepository ipAddressRepository, ICountryRepository countryRepository, IIP2CountryService ip2CountryService, IMemoryCache cache)
        {
            _logger = logger;
            _ipAddressRepository = ipAddressRepository;
            _countryRepository = countryRepository;
            _ip2CountryService = ip2CountryService;
            _cache = cache;
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        { 
            DateTime startDate = DateTime.UtcNow;

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Invalidate Cache Service stated running: {DateTime.Now}");

                int batchSize = 100;
                int total = await _ipAddressRepository.GetTotalCountIpAddresses();

                for (int i = 0; i <= total / batchSize; i++)
                {
                    foreach (var ip in (await _ipAddressRepository.GetAllIpAddresses()).Skip(i * batchSize).Take(batchSize).ToList())
                    {
                        _logger.LogInformation($"Checkin ip: {ip}");

                        var country = await _ip2CountryService.GetCountryAsync(ip.IP);
                        var dbCountry = await _countryRepository.FindCountryByNameAsync(country.Name);

                        // check if IP2C return data to IP from database
                        if (country == null)
                        {
                            _logger.LogWarning($"No data from IP2C ip {ip}.");
                            continue;
                        }

                        // add country to database if not exists
                        if (dbCountry == null)
                        {
                            _countryRepository.AddCountry(country);
                        }
                        else
                        {
                            if (!country.Equals(dbCountry))
                            {
                                _cache?.Remove(ip.IP);
                                _logger.LogInformation($"Cache for country {country} cleared.");

                                // update country in database
                                dbCountry.Name = country.Name;
                                dbCountry.TwoLetterCode = country.TwoLetterCode;
                                dbCountry.ThreeLetterCode = country.ThreeLetterCode;

                                _countryRepository.SaveCountry(dbCountry);

                                ip.UpdatedAt = DateTime.UtcNow;

                                await _ipAddressRepository.SaveIPAddress(ip);
                            }
                        }
                    }
                }

                // calculate the amount of time in order to execute every one hour 
                TimeSpan interval = DateTime.UtcNow - startDate;
                int timeToNextExecutionInSeconds = 360 - interval.Seconds;

                if (timeToNextExecutionInSeconds > 0)
                {
                    await Task.Delay(timeToNextExecutionInSeconds * 1000, stoppingToken);
                }
            }
        }
    }
}
