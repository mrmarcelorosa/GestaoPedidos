using GestaoPedidos.DTO;
using GestaoPedidos.Exceptions;
using GestaoPedidos.Models;
using GestaoPedidos.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Retorna todos os clientes.
        /// </summary>
        /// <returns>Lista de clientes.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Client>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetAllAsync();
            return Ok(clients);
        }

        /// <summary>
        /// Retorna um cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <returns>Detalhes do cliente.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientService.GetByIdAsync(id);
            if (client == null)
                return NotFound();
            return Ok(client);
        }

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="client">Dados do cliente.</param>
        /// <returns>Cliente criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Client), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            if (client == null || client.Addresses == null || client.Addresses.Count == 0)
            {
                throw new ApiExceptions(400, "Cliente ou lista de endereços vazia");
            }

            await _clientService.AddClientWithAddressesAsync(client);

            return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
        }

        /// <summary>
        /// Atualiza os dados de um cliente.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <param name="client">Dados atualizados do cliente.</param>
        /// <returns>Sem conteúdo.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] Client client)
        {
            if (id != client.Id)
                return BadRequest();

            await _clientService.UpdateAsync(client);
            return NoContent();
        }

        /// <summary>
        /// Exclui um cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <returns>Sem conteúdo.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Retorna clientes e endereços para preencher selects(dropdowns).
        /// </summary>
        /// <returns>Lista de clientes/endereços.</returns>
        [HttpGet("selects")]
        [ProducesResponseType(typeof(List<ClientAddressDTO>), 200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetClientAddresses()
        {
            var clientAddresses = await _clientService.GetClientAddressesAsync();
            if (clientAddresses == null || !clientAddresses.Any())
            {
                return Ok(new List<ClientAddressDTO>());
            }

            return Ok(clientAddresses);
        }
    }
}
