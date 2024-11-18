/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       10/22/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using IPInfo.Entities.Model;

namespace IPInfo.Entities.Repository
{
    public interface IIPAddressRepository
    {
        public Task<IPAddress> FindIPAddressByIPAsync(string ipAddress);

        public void AddIpAddress(IPAddress ipAddress);

        public Task<List<IPAddress>> GetAllIpAddresses();

        public Task<int> GetTotalCountIpAddresses();
        public Task SaveIPAddress(IPAddress ipAddress);
    }
}
