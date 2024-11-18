/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       11/04/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using IPInfo.DataAccess.EntityFramework;
using IPInfo.DataAccess.EntityFramework.SqlServer;

namespace IPInfo
{
    public class DataContextFactory
    {
        public static IPInfoAbstractDataContext CreateIPInfoDataContext()
        {
            string database = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Database").Value;
            var connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["SqlServer"];

            switch (database)
            {
                case "SqlServer":
                    return new IPInfoSqlServerDataContext(connectionString);

                default:
                    return new IPInfoSqlServerDataContext(connectionString);
            }

        }
}
}
