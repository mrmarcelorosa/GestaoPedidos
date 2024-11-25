using GestaoPedidos.Data;
using GestaoPedidos.Models;
using GestaoPedidos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace GestaoPedidos.Repositories.Implementations
{
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private readonly AppDbContext _context;

        public OrderItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            string query = "select * from order_item where date_delete is null";
            var formattableQuery = FormattableStringFactory.Create(query);
            return await _context.OrderItem.FromSqlInterpolated(formattableQuery).ToListAsync();
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _context.OrderItem.FindAsync(id);
        }

        public async Task AddAsync(OrderItem entity)
        {
            await _context.OrderItem.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderItem entity)
        {
            _context.OrderItem.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var orderItem = await _context.OrderItem.FirstOrDefaultAsync(e => e.Id == id);
            if (orderItem != null)
            {
                orderItem.DateDelete = DateTime.UtcNow;
                _context.OrderItem.Update(orderItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddRangeAsync(IEnumerable<OrderItem> entities)
        {
            await _context.OrderItem.AddRangeAsync(entities);  
            await _context.SaveChangesAsync(); 
        }
    }
}
