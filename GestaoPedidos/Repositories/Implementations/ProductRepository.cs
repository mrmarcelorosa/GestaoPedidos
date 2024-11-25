using GestaoPedidos.Data;
using GestaoPedidos.DTO;
using GestaoPedidos.Models;
using GestaoPedidos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace GestaoPedidos.Repositories.Implementations
{
    public class ProductRepository : IRepository<Product>
    {

        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            string query = "select * from product where date_delete is null";
            var formattableQuery = FormattableStringFactory.Create(query);
            return await _context.Product.FromSqlInterpolated(formattableQuery).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Product.FindAsync(id);
        }

        public async Task AddAsync(Product entity)
        {
            await _context.Product.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _context.Product.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Product.FirstOrDefaultAsync(e => e.Id == id);
            if (product != null)
            {
                product.DateDelete = DateTime.UtcNow;
                _context.Product.Update(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddRangeAsync(IEnumerable<Product> entities)
        {
            await _context.Product.AddRangeAsync(entities);  
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductDTO>> GetProductsSelectAsync()
        {
            var products = await _context.Product
                .Where(p => p.DateDelete == null)
                .Select(p => new ProductDTO
                {
                    ProductId = p.Id,
                    ProductName = p.Name
                })
                .ToListAsync();

            return products;
        }
    }
}
