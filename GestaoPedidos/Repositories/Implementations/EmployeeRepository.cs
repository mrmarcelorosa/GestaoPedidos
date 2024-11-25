using GestaoPedidos.Data;
using GestaoPedidos.Models;
using GestaoPedidos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace GestaoPedidos.Repositories.Implementations
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            string query = "select * from people where date_delete is null";
            var formattableQuery = FormattableStringFactory.Create(query);
            return await _context.Employee.FromSqlInterpolated(formattableQuery).ToListAsync();
        }


        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employee.Where(e => e.Id == id && e.DateDelete == null).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Employee entity)
        {
            await _context.Employee.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee entity)
        {
            _context.Employee.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employee.FirstOrDefaultAsync(e => e.Id == id);
            if (employee != null)
            {
                employee.DateDelete = DateTime.UtcNow;
                _context.Employee.Update(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddRangeAsync(IEnumerable<Employee> entities)
        {
            await _context.Employee.AddRangeAsync(entities);  
            await _context.SaveChangesAsync();  
        }
    }
}
