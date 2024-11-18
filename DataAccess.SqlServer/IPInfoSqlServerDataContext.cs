/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       11/04/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using IPInfo.Entities.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore; 

namespace IPInfo.DataAccess.EntityFramework.SqlServer
{
    public class IPInfoSqlServerDataContext : IPInfoAbstractDataContext
    {
        private readonly string _connectionString;
        public IPInfoSqlServerDataContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        public override List<CountrySummary> GetCountriesSummary(string[] twoLetterCodes)
        {
            string query = $@"
                  
                    select
	                    Countries.Name CountryName,
	                    count(Countries.Id) AddressesCount,
	                    max(IPAddresses.UpdatedAt) LastAddressUpdated
                    from IPAddresses 
                    left join Countries on IPAddresses.CountryId = Countries.Id
                    where 1=1
                    {(twoLetterCodes.Length > 0 ? $"and TwoLetterCode in ({string.Join(',', twoLetterCodes.Select(x => $"'{x}'"))})" : string.Empty)}
                    group by Name
                    order by count(Countries.Id) desc 
                
            ";
            List<CountrySummary> result = new List<CountrySummary>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        result.Add(new CountrySummary()
                        {
                            CountryName = reader["CountryName"].ToString(),
                            AddressesCount = Convert.ToInt32(reader["AddressesCount"]),
                            LastAddressUpdated = Convert.ToDateTime(reader["LastAddressUpdated"])
                        });
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return result;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
