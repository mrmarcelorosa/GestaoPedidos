using GestaoPedidos.DTO;
using GestaoPedidos.Models;
using GestaoPedidos.Repositories.Implementations;
using GestaoPedidos.Repositories.Interfaces;

namespace GestaoPedidos.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository) 
        { 
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Product produto)
        {
            await _productRepository.AddAsync(produto);
        }

        public async Task UpdateAsync(Product produto)
        {
            await _productRepository.UpdateAsync(produto);
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<List<ProductDTO>> GetProductsSelectAsync()
        {
            return await _productRepository.GetProductsSelectAsync();
        }
    }
}
