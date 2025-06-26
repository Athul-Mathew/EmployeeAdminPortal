using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var AllEmployees=dbContext.Employees.ToList();
            return Ok(AllEmployees);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id) {
            var employee = dbContext.Employees.FirstOrDefault(e => e.id == id);
            if (employee == null)
            {
                return BadRequest();
            }

            return Ok();

        }
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var EmployeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
            };
            dbContext.Employees.Add(EmployeeEntity);
            dbContext.SaveChanges();
            return Ok(EmployeeEntity);
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult EditEmployee(Guid id,AddEmployeeDto dto)
        {
            var employee = dbContext.Employees.FirstOrDefault(i => i.id == id);
            if (employee == null)
            {
                return NotFound();

            }

            employee.Email = dto.Email;
            employee.Name = dto.Name;
            employee.Phone = dto.Phone;
            employee.Salary = dto.Salary;

            dbContext.SaveChanges();
            return Ok();
        }

    }
}
