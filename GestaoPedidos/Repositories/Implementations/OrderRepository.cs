using GestaoPedidos.Data;
using GestaoPedidos.DTO;
using GestaoPedidos.Models;
using GestaoPedidos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace GestaoPedidos.Repositories.Implementations
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            string query = "select * from order where date_delete is null";
            var formattableQuery = FormattableStringFactory.Create(query);
            return await _context.Order.FromSqlInterpolated(formattableQuery).ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Order.FindAsync(id);
        }

        public async Task AddAsync(Order entity)
        {
            await _context.Order.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order entity)
        {
            _context.Order.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Order.FirstOrDefaultAsync(e => e.Id == id);
            if (order != null)
            {
                order.DateDelete = DateTime.UtcNow;
                _context.Order.Update(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddRangeAsync(IEnumerable<Order> entities)
        {
            await _context.Order.AddRangeAsync(entities);  
            await _context.SaveChangesAsync(); 
        }

        public async Task<List<OrderHeaderDTO>> GetOrderHeadersAsync(
            string? clientName = null,
            int? orderId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var query = from o in _context.Order
                        join c in _context.Client on o.ClientId equals c.Id
                        join ba in _context.Address on o.BillingAddressId equals ba.Id
                        join da in _context.Address on o.DeliveryAddressId equals da.Id
                        where o.DateDelete == null
                        select new OrderHeaderDTO
                        {
                            OrderId = o.Id,
                            OrderDate = o.OrderDate,
                            ClientName = c.Name,
                            ClientEmail = c.Email,
                            BillingAddress = ba.Logradouro + ", " + ba.NumberAddres + " - " + ba.City + ", " + ba.State,
                            DeliveryAddress = da.Logradouro + ", " + da.NumberAddres + " - " + da.City + ", " + da.State,
                            TotalPrice = _context.OrderItem
                                .Where(oi => oi.OrderId == o.Id && oi.DateDelete == null)
                                .Sum(oi => oi.UnitPrice * oi.Amount)
                        };

            if (!string.IsNullOrEmpty(clientName))
            {
                query = query.Where(o => o.ClientName.Contains(clientName));
            }
            if (orderId.HasValue)
            {
                query = query.Where(o => o.OrderId == orderId.Value);
            }
            if (startDate.HasValue)
            {
                query = query.Where(o => o.OrderDate >= startDate.Value.ToUniversalTime());
            }
            if (endDate.HasValue)
            {
                query = query.Where(o => o.OrderDate <= endDate.Value.ToUniversalTime());
            }

            return await query.ToListAsync();
        }


        public async Task<List<OrderDetailDTO>> GetOrderDetailsAsync(int orderId)
        {
            var orderDetails = from oi in _context.OrderItem
                               join p in _context.Product on oi.ProductId equals p.Id
                               join o in _context.Order on oi.OrderId equals o.Id
                               where o.Id == orderId && o.DateDelete == null
                               select new OrderDetailDTO
                               {
                                   ProductId = oi.ProductId,
                                   ProductName = p.Name,
                                   Quantity = oi.Amount,
                                   UnitPrice = oi.UnitPrice
                               };

            return await orderDetails.ToListAsync();
        }

        public async Task<OrderSummaryDTO> GetOrderSummaryAsync(int orderId)
        {
            var orderSummary = from o in _context.Order
                               join c in _context.Client on o.ClientId equals c.Id
                               join ba in _context.Address on o.BillingAddressId equals ba.Id
                               join da in _context.Address on o.DeliveryAddressId equals da.Id
                               where o.Id == orderId && o.DateDelete == null
                               select new OrderSummaryDTO
                               {
                                   TotalOrderValue = o.OrderItems.Sum(oi => oi.UnitPrice * oi.Amount),
                                   ClientName = c.Name,
                                   ClientEmail = c.Email,
                                   BillingAddress = ba.Logradouro + ", " + ba.NumberAddres + " - " + ba.City + ", " + ba.State,
                                   DeliveryAddress = da.Logradouro + ", " + da.NumberAddres + " - " + da.City + ", " + da.State,
                                   OrderDate = o.OrderDate,
                                   OrderDetails = (from oi in _context.OrderItem
                                                   join p in _context.Product on oi.ProductId equals p.Id
                                                   where oi.OrderId == o.Id
                                                   select new OrderDetailDTO
                                                   {
                                                       ProductId = oi.ProductId,
                                                       ProductName = p.Name,
                                                       Quantity = oi.Amount,
                                                       UnitPrice = oi.UnitPrice,
                                                   }).ToList()
                               };

            return await orderSummary.SingleOrDefaultAsync();
        }
    }
}                       