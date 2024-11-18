/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       11/04/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/
 
using IPInfo.Entities.Model;
using IPInfo.Entities.Repository;
using IPInfo.Entities.Service;
using IPInfo.ApiControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace IPInfo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IPInfoController : ControllerBase
    {
        private readonly IIPAddressRepository _ipAddressRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IIP2CountryService _ip2CountryService;
        private readonly ILogger<IPInfoController> _logger;
        private readonly IMemoryCache _cache;

        public IPInfoController(ILogger<IPInfoController> logger, IMemoryCache cache, IIPAddressRepository ipAddressRepository, ICountryRepository countryRepository, IIP2CountryService ip2CountryService)
        {
            _logger = logger;
            _cache = cache;
            _ipAddressRepository = ipAddressRepository;
            _countryRepository = countryRepository;
            _ip2CountryService = ip2CountryService;
        }

        [HttpGet("{ip}")]
        public async Task<ActionResult<Country>> GetIPCountry(string ip)
        {
            try
            {
                IP2Country ip2Country = new IP2Country(_logger, _cache, _ipAddressRepository, _countryRepository, _ip2CountryService);
                return await ip2Country.GetIPCountry(ip);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
