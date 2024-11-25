using GestaoPedidos.Models;
using GestaoPedidos.Repositories.Implementations;
using GestaoPedidos.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoPedidos.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Employee employee)
        {
            await _employeeRepository.AddAsync(employee);
        }

        public async Task UpdateAsync(Employee employee)
        {
            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task DeleteAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }
    }
}
