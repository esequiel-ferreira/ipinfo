using IPInfo.Entities.Model;
using IPInfo.Entities.Repository;

namespace IPInfo.Tests.Repository.Fake
{
    public class FakeCountryRepository : ICountryRepository
    {
        private List<Country> Countries { get; set; } = new List<Country>();

        public void AddCountry(Country country)
        {
            Countries.Add(country);
        }

        public async Task<Country> FindCountryByIDAsync(int id)
        {
            return Countries.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Country> FindCountryByNameAsync(string name)
        {
            return Countries.FirstOrDefault(x => x.Name == name);
        }

        public Task SaveCountry(Country country)
        {
            return Task.CompletedTask;
        }
    }
}
