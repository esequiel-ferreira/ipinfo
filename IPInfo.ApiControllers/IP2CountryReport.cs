/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       11/04/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using IPInfo.Entities.Model;
using IPInfo.Entities.Report;

namespace IPInfo.ApiControllers
{
    public class IP2CountryReport
    {
        private readonly IReport _report;
        public IP2CountryReport(IReport report)
        {
            _report = report;
        }

        public List<CountrySummary> GetCountriesSummary(string[] twoLetterCodes)
        {
            return _report.GetCountriesSummary(twoLetterCodes);
        }
    }
}
