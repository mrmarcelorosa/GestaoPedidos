using GestaoPedidos.Models;
using GestaoPedidos.Repositories.Implementations;

namespace GestaoPedidos.Services
{
    public class AddressService
    {
        private readonly AddressRepository _addressRepository;

        public AddressService(AddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _addressRepository.GetAllAsync();
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            return await _addressRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Address address)
        {
            await _addressRepository.AddAsync(address);
        }

        public async Task AddRangeAsync(List<Address> address)
        {
            await _addressRepository.AddRangeAsync(address);
        }

        public async Task UpdateAsync(Address address)
        {
            await _addressRepository.UpdateAsync(address);
        }

        public async Task DeleteAsync(int id)
        {
            await _addressRepository.DeleteAsync(id);
        }

        public async Task SaveAddressesAsync(List<Address> addresses)
        {
            foreach (var address in addresses)
            {
                await _addressRepository.AddAsync(address);
            }
        }
    }
}
