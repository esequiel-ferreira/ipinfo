using IPInfo.Entities.Model;
using IPInfo.Tests.Repository.Fake;
using IPInfo.ApiControllers;

namespace IPInfo.Tests
{
    public class ReportTest
    {
        [Fact]
        public void Report()
        {
            // arrange
            FakeReport report = new FakeReport()
            {
                CountrySummaries = new List<CountrySummary>()
                {
                    new CountrySummary(){ AddressesCount = 1, CountryName = "Brazil", LastAddressUpdated = new DateTime(2021, 12, 1)},
                    new CountrySummary(){ AddressesCount = 3, CountryName = "Greece", LastAddressUpdated =  new DateTime(2023, 1, 3) },
                }
            };

            IP2CountryReport IP2CountryReport = new IP2CountryReport(report);

            // act
            var sut = IP2CountryReport.GetCountriesSummary([]);


            // assert
            Assert.True(sut.Count == 2 && sut.ElementAt(1).CountryName == "Greece");
        }
    }
}