using GestaoPedidos.Data;
using GestaoPedidos.Models;
using GestaoPedidos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace GestaoPedidos.Repositories.Implementations
{
    public class AddressRepository : IRepository<Address>
    {
        private readonly AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            string query = "select * from address where date_delete is null";
            var formattableQuery = FormattableStringFactory.Create(query);
            return await _context.Address.FromSqlInterpolated(formattableQuery).ToListAsync();
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            return await _context.Address.FindAsync(id);
        }

        public async Task AddAsync(Address entity)
        {
            await _context.Address.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Address entity)
        {
            _context.Address.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var address = await _context.Address.FirstOrDefaultAsync(e => e.Id == id);
            if (address != null)
            {
                address.DateDelete = DateTime.UtcNow;
                _context.Address.Update(address);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddRangeAsync(IEnumerable<Address> entities)
        {
            await _context.Address.AddRangeAsync(entities);
            await _context.SaveChangesAsync();  
        }
    }

}
