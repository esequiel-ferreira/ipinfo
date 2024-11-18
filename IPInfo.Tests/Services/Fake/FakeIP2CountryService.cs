using IPInfo.Entities.Model;
using IPInfo.Entities.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPInfo.Tests.Services.Fake
{
    public class FakeIP2CountryService : IIP2CountryService
    {
        public async Task<Country> GetCountryAsync(string ip)
        {
            return new Country()
            {
                CreatedAt = DateTime.UtcNow,
                Id = 1,
                Name = "Brazil",
                TwoLetterCode = "BR",
                ThreeLetterCode = "BRA"
            };
        }
    }
}
