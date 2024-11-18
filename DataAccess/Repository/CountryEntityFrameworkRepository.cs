using IPInfo.Entities.Model;
using IPInfo.Entities.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPInfo.DataAccess.EntityFramework.Repository
{
    public class CountryEntityFrameworkRepository : ICountryRepository
    {
        private readonly IPInfoAbstractDataContext _context;

        public CountryEntityFrameworkRepository(IPInfoAbstractDataContext context)
        {
            _context = context;
        }

        public async Task<Country> FindCountryByIDAsync(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task<Country> FindCountryByNameAsync(string name)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Name == name);
        }

        public void AddCountry(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();
        }

        public async Task SaveCountry(Country country)
        {
            throw new NotImplementedException();
        }
    }
}
