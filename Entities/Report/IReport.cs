/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       11/04/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using IPInfo.Entities.Model;

namespace IPInfo.Entities.Report
{
    public interface IReport
    {
        List<CountrySummary> GetCountriesSummary(string[] twoLetterCodes);
    }
}
