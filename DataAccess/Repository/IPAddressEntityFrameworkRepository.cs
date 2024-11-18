using IPInfo.Entities.Model;
using IPInfo.Entities.Repository;
using Microsoft.EntityFrameworkCore;

namespace IPInfo.DataAccess.EntityFramework.Repository
{
    public class IPAddressEntityFrameworkRepository : IIPAddressRepository
    {
        private readonly IPInfoAbstractDataContext _context;

        public IPAddressEntityFrameworkRepository(IPInfoAbstractDataContext context)
        {
            _context = context;
        }

        public void AddIpAddress(IPAddress ipAddress)
        {
            _context.IPAddresses.Add(ipAddress);
            _context.SaveChanges();
        }

        public async Task<IPAddress> FindIPAddressByIPAsync(string ipAddress)
        {
            return await _context.IPAddresses.FirstOrDefaultAsync(i => i.IP == ipAddress);
        }

        public async Task<List<IPAddress>> GetAllIpAddresses()
        {
            return await _context.IPAddresses.ToListAsync();
        }

        public async Task<int> GetTotalCountIpAddresses()
        {
            return await _context.IPAddresses.CountAsync();
        }

        public async Task SaveIPAddress(IPAddress ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}
