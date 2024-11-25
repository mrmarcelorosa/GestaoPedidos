using GestaoPedidos.Models;
using GestaoPedidos.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest();

            await _employeeService.AddAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            await _employeeService.UpdateAsync(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
