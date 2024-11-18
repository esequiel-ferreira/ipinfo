using IPInfo.ApiControllers;
using IPInfo.Entities.Model;
using IPInfo.Entities.Repository;
using IPInfo.Entities.Service;
using IPInfo.Tests.Repository.Fake;
using IPInfo.Tests.Services.Fake;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPInfo.Tests
{
    public class IP2CountryTests
    {
        [Fact]
        public async void Run_Action_With_No_Cache_and_No_Register_in_Database()
        {
            // arrange
            ICountryRepository fakeCountryRepository = new FakeCountryRepository();
            IIPAddressRepository fakeIPAddressRepository = new FakeIPAddressRepository();
            IIP2CountryService ip2CountryService = new FakeIP2CountryService();

            using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder.SetMinimumLevel(LogLevel.Trace));
            ILogger logger = loggerFactory.CreateLogger<IP2CountryTests>();
            IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

            IP2Country ip2Country = new IP2Country(logger, memoryCache, fakeIPAddressRepository, fakeCountryRepository, ip2CountryService);
            string ipAddress = "104.246.80.205";

            // act
            var task = await ip2Country.GetIPCountry(ipAddress);
            var ip = await fakeIPAddressRepository.FindIPAddressByIPAsync(ipAddress);
            var country = await fakeCountryRepository.FindCountryByNameAsync("Brazil");

            //assert
            Assert.NotNull(ip);
            Assert.NotNull(country);
        }

        [Fact]
        public async void Run_Action_With_Cache_and_Register_in_Database()
        {
            // arrange
            ICountryRepository fakeCountryRepository = new FakeCountryRepository();
            IIPAddressRepository fakeIPAddressRepository = new FakeIPAddressRepository();
            IIP2CountryService ip2CountryService = new FakeIP2CountryService();

            using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder.SetMinimumLevel(LogLevel.Trace));
            ILogger logger = loggerFactory.CreateLogger<IP2CountryTests>();

            IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

            IP2Country ip2Country = new IP2Country(logger, memoryCache, fakeIPAddressRepository, fakeCountryRepository, ip2CountryService);
            string ipAddress = "104.246.80.205";

            memoryCache.Set(ipAddress, new Country()
            {
                CreatedAt = DateTime.UtcNow,
                Id = 1,
                Name = "Greece",
                TwoLetterCode = "GR",
                ThreeLetterCode = "GRE"
            });

            // act
            var sut = await ip2Country.GetIPCountry(ipAddress);

            var ip = await fakeIPAddressRepository.FindIPAddressByIPAsync(ipAddress);
            var country = await fakeCountryRepository.FindCountryByNameAsync("Greece");

            //assert
            Assert.Null(ip);
            Assert.Null(country);
        }
    }
}
