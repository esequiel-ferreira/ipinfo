using IPInfo.Entities.Model;
using IPInfo.Entities.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPInfo.Tests.Repository.Fake
{
    public class FakeReport : IReport
    {
        public List<CountrySummary> CountrySummaries { get; set; } = new List<CountrySummary>();
         
        public List<CountrySummary> GetCountriesSummary(string[] twoLetterCodes)
        {
            return CountrySummaries;
        }
    }
}
