/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       10/22/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using IPInfo.Entities.Model;

namespace IPInfo.Entities.Repository
{
    public interface ICountryRepository
    {
        public Task<Country> FindCountryByIDAsync(int id);
        public Task<Country> FindCountryByNameAsync(string name);
        public void AddCountry(Country country);
        public Task SaveCountry(Country country);
    }
}
