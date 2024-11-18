/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       11/04/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using IP2C.org; 
using IPInfo.Entities.Service;

namespace IPInfo
{
    public class IP2CountryFactory
    {
        public static IIP2CountryService CreateIP2CountryService()
        {
            string database = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("IP2CService").Value; 

            switch (database)
            {
                case "IP2C.org":
                    return new IP2CService();

                default:
                    return new IP2CService();
            }

        }
}
}
