/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       11/04/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using IPInfo.Entities.Model;
using IPInfo.Entities.Report;
using Microsoft.EntityFrameworkCore;

namespace IPInfo.DataAccess.EntityFramework
{
    public abstract class IPInfoAbstractDataContext : DbContext, IReport
    {
        public IPInfoAbstractDataContext(DbContextOptions options) : base(options) { }
        public IPInfoAbstractDataContext() : base() { }
        public DbSet<IPAddress> IPAddresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public abstract List<CountrySummary> GetCountriesSummary(string[] twoLetterCodes);
    }
}
