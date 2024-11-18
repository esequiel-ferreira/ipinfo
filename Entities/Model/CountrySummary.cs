/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       10/22/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

namespace IPInfo.Entities.Model
{
    public class CountrySummary
    {
        public string CountryName { get; set; } = string.Empty;
        public int AddressesCount { get; set; }
        public DateTime LastAddressUpdated { get; set; }
    }
}
