using GestaoPedidos.Data;
using GestaoPedidos.DTO;
using GestaoPedidos.Models;
using GestaoPedidos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Runtime.CompilerServices;

namespace GestaoPedidos.Repositories.Implementations
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            string query = "select * from people where date_delete is null";
            var formattableQuery = FormattableStringFactory.Create(query);
            return await _context.Client.FromSqlInterpolated(formattableQuery).Include(c => c.Addresses).ToListAsync();
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _context.Client
            .Include(c => c.Addresses)
            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Client entity)
        {
            await _context.Client.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client entity)
        {
            _context.Client.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _context.Client.FirstOrDefaultAsync(e => e.Id == id);
            if (client != null)
            {
                client.DateDelete = DateTime.UtcNow;
                _context.Client.Update(client);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Client> entities)
        {
            await _context.Client.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ClientAddressDTO>> GetClientAddressesAsync()
        {
            var clientAddresses = await _context.Client
                .Where(c => c.DateDelete == null)
                .Select(c => new ClientAddressDTO
                {
                    ClientId = c.Id,
                    ClientName = c.Name,
                    Addresses = c.Addresses
                        .Where(a => a.DateDelete == null) 
                        .Select(a => new ClientAddressDetailDTO
                        {
                            AddressId = a.Id,
                            AddressInfo = a.Logradouro + ", " + a.NumberAddres + " - " + a.City + ", " + a.State
                        })
                        .Distinct()
                        .ToList()
                })
                .ToListAsync();

            return clientAddresses;
        }


    }
}
