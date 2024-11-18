/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       10/22/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using IPInfo.Entities.Model;

namespace IPInfo.Entities.Service
{
    public interface IIP2CountryService
    {
        Task<Country> GetCountryAsync(string ip);
    }
}
