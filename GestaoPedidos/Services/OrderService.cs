using GestaoPedidos.DTO;
using GestaoPedidos.Exceptions;
using GestaoPedidos.Models;
using GestaoPedidos.Repositories.Implementations;
using GestaoPedidos.Request;

namespace GestaoPedidos.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly ProductService _productService;

        public OrderService(OrderRepository orderRepository, ProductService productService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<Order> AddAsync(OrderCreateRequest orderRequest)
        {
            var order = new Order
            {
                ClientId = orderRequest.ClientId,
                BillingAddressId = orderRequest.BillingAddressId,
                DeliveryAddressId = orderRequest.DeliveryAddressId,
                OrderDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>()
            };

            foreach (var itemRequest in orderRequest.OrderItems)
            {
                Product _product = await _productService.GetByIdAsync(itemRequest.ProductId);
                var orderItem = new OrderItem
                {
                    ProductId = itemRequest.ProductId,
                    UnitPrice = _product.Price,
                    Amount = itemRequest.Amount,
                    OrderId = order.Id
                };
                order.OrderItems.Add(orderItem);
            }

            await _orderRepository.AddAsync(order);

            foreach(var orderItem in order.OrderItems)
            {
                orderItem.OrderId = order.Id;
            }

            return order;

        }

        public async Task UpdateAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
        }

        public async Task<List<OrderHeaderDTO>> GetOrderHeadersAsync(
            string? clientName = null,
            int? orderId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            return await _orderRepository.GetOrderHeadersAsync(clientName,orderId,startDate,endDate);
        }

        public async Task<List<OrderDetailDTO>> GetOrderDetailsAsync(int orderId)
        {
            return await _orderRepository.GetOrderDetailsAsync(orderId);
        }

        public async Task<OrderSummaryDTO> GetOrderSummaryAsync(int orderId)
        {
            return await _orderRepository.GetOrderSummaryAsync(orderId);
        }

        public async Task<byte[]> ExportOrdersToExcelAsync(int orderId)
        {
            var orderSumary = await GetOrderSummaryAsync(orderId);
            
            var excelExporter = new ExcelExporter();

            var excelData = excelExporter.ExportOrderSummaryToExcel(orderSumary);

            return excelData;

        }
    }
}
