using GestaoPedidos.Models;
using GestaoPedidos.Repositories.Implementations;

namespace GestaoPedidos.Services
{
    public class OrderItemService
    {
        private readonly OrderItemRepository _orderItemRepository;

        public OrderItemService(OrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _orderItemRepository.GetAllAsync();
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            return await _orderItemRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(OrderItem orderItem)
        {
            await _orderItemRepository.AddAsync(orderItem);
        }

        public async Task UpdateAsync(OrderItem orderItem)
        {
            await _orderItemRepository.UpdateAsync(orderItem);
        }

        public async Task DeleteAsync(int id)
        {
            await _orderItemRepository.DeleteAsync(id);
        }
    }
}
