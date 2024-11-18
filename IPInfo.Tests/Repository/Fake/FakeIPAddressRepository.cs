using IPInfo.Entities.Model;
using IPInfo.Entities.Repository;

namespace IPInfo.Tests.Repository.Fake
{
    public class FakeIPAddressRepository : IIPAddressRepository
    {
        private List<IPAddress> IPAddresses { get; set; } = new List<IPAddress>();

        public void AddIpAddress(IPAddress ipAddress)
        {
            IPAddresses.Add(ipAddress);
        }

        public async Task<IPAddress> FindIPAddressByIPAsync(string ipAddress)
        {
            return IPAddresses.FirstOrDefault(x => x.IP == ipAddress);
        }

        public async Task<List<IPAddress>> GetAllIpAddresses()
        {
            return IPAddresses;
        }

        public async Task<int> GetTotalCountIpAddresses()
        {
            return IPAddresses.Count;
        }

        public Task SaveIPAddress(IPAddress ipAddress)
        {
            return Task.CompletedTask;
        }
    }
}
