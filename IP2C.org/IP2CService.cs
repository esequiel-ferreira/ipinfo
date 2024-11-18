/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       11/04/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using IPInfo.Entities.Model;
using IPInfo.Entities.Service;

namespace IP2C.org
{
    public class IP2CService : IIP2CountryService
    {
        private readonly HttpClient client = new HttpClient();
        public async Task<Country> GetCountryAsync(string ip)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await client.GetAsync("https://ip2c.org/" + ip);
            }
            catch (Exception ex) { throw new Exception($"Fail to connecto to IP2C. {ex.Message}", ex); }

            var content = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(content) || content.Contains(';') || content.Split(';').Length >= 4)
            {
                var values = content.Split(';');
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        return new Country()
                        {
                            TwoLetterCode = values[1],
                            ThreeLetterCode = values[2],
                            Name = values[3].Truncate(50),
                            CreatedAt = DateTime.UtcNow
                        };
                    }
                    catch (Exception ex) { throw new Exception($"Fail to parse IP2C response. {ex.Message}", ex); }
                }
                else { throw new Exception("IP2C returned an error: " + content.Split(';')[3]); }
            }
            else { return null; }

        }
    }
}
