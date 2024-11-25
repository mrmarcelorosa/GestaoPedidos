using GestaoPedidos.DTO;
using GestaoPedidos.Models;
using GestaoPedidos.Request;
using GestaoPedidos.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPedidos.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Retorna todos os pedidos.
        /// </summary>
        /// <returns>Lista de pedidos.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Order>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Retorna os detalhes de um pedido pelo ID.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        /// <returns>Detalhes do pedido.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        /// <summary>
        /// Adiciona um novo pedido.
        /// </summary>
        /// <param name="orderRequest">Dados do pedido.</param>
        /// <returns>Pedido criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Order), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add([FromBody] OrderCreateRequest orderRequest)
        {
            var order = await _orderService.AddAsync(orderRequest);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        /// <summary>
        /// Atualiza os dados de um pedido existente.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        /// <param name="order">Dados atualizados do pedido.</param>
        /// <returns>Sem conteúdo.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(int id, [FromBody] Order order)
        {
            if (id != order.Id)
                return BadRequest();

            await _orderService.UpdateAsync(order);
            return NoContent();
        }

        /// <summary>
        /// Remove um pedido pelo ID.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        /// <returns>Sem conteúdo.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Retorna os cabeçalhos de pedidos filtrados por parâmetros opcionais.
        /// </summary>
        /// <param name="clientName">Nome do cliente.</param>
        /// <param name="orderId">ID do pedido.</param>
        /// <param name="startDate">Data inicial.</param>
        /// <param name="endDate">Data final.</param>
        /// <returns>Lista de cabeçalhos de pedidos.</returns>
        [HttpGet("headers")]
        [ProducesResponseType(typeof(List<OrderHeaderDTO>), 200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetOrderHeadersAsync(
            [FromQuery] string? clientName = null,
            [FromQuery] int? orderId = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            var orders = await _orderService.GetOrderHeadersAsync(clientName, orderId, startDate, endDate);
            if (orders == null || !orders.Any())
            {
                return Ok(new List<OrderHeaderDTO>());
            }
            return Ok(orders);
        }

        /// <summary>
        /// Retorna os detalhes de um pedido pelo ID.
        /// </summary>
        /// <param name="orderId">ID do pedido.</param>
        /// <returns>Lista de detalhes do pedido.</returns>
        [HttpGet("{orderId}/details")]
        [ProducesResponseType(typeof(List<OrderDetailDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var orderDetails = await _orderService.GetOrderDetailsAsync(orderId);
            if (orderDetails == null || !orderDetails.Any())
            {
                return NotFound();
            }
            return Ok(orderDetails);
        }

        /// <summary>
        /// Retorna o resumo de um pedido pelo ID.
        /// </summary>
        /// <param name="orderId">ID do pedido.</param>
        /// <returns>Resumo do pedido.</returns>
        [HttpGet("{orderId}/sumary")]
        [ProducesResponseType(typeof(OrderSummaryDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOrderSummaryAsync(int orderId)
        {
            var orderDetails = await _orderService.GetOrderSummaryAsync(orderId);
            if (orderDetails == null)
            {
                return NotFound();
            }
            return Ok(orderDetails);
        }

        /// <summary>
        /// Exporta o resumo de um pedido para um arquivo Excel.
        /// </summary>
        /// <param name="orderId">ID do pedido.</param>
        /// <returns>Arquivo Excel para download.</returns>
        [HttpGet("{orderId}/export")]
        [ProducesResponseType(typeof(FileResult), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ExportOrderSummaryToExcel(int orderId)
        {
            try
            {
                var excelData = await _orderService.ExportOrdersToExcelAsync(orderId);

                // Retornar o arquivo para download
                return File(
                    excelData,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Order_{orderId}_Summary.xlsx");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao gerar o arquivo.");
            }
        }
    }
}
