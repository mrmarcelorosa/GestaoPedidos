using GestaoPedidos.DTO;
using GestaoPedidos.Exceptions;
using GestaoPedidos.Models;
using GestaoPedidos.Repositories.Implementations;

namespace GestaoPedidos.Services
{
    public class ClientService
    {
        private readonly ClientRepository _clientRepository;

        private readonly AddressService _addressService;


        public ClientService(ClientRepository clientRepository, AddressService addressService)
        {
            _clientRepository = clientRepository;
            _addressService = addressService;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _clientRepository.GetAllAsync();
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _clientRepository.GetByIdAsync(id);
        }
        public async Task AddClientWithAddressesAsync(Client client)
        {
            if (!client.CPFIsValid())
                throw new ApiExceptions(409, "CPF Inválido");

            await _clientRepository.AddAsync(client);

            if (client.Addresses != null && client.Addresses.Any())
            {
                foreach (var address in client.Addresses)
                {
                    address.ClientId = client.Id;
                }

            }
        }

        public async Task UpdateAsync(Client client)
        {
            await _clientRepository.UpdateAsync(client);
        }

        public async Task DeleteAsync(int id)
        {
            await _clientRepository.DeleteAsync(id);
        }

        public async Task<List<ClientAddressDTO>> GetClientAddressesAsync()
        {
            return await _clientRepository.GetClientAddressesAsync();
        }
    }
}
