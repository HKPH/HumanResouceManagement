using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        [HttpGet("DOB")]
        public IActionResult GetEmployeesDOB([FromQuery] int days)
        {    
            if (days < 0)
            {
                return BadRequest("Days cannot be negative.");
            }
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetEmployeesBirthday(days));
            return Ok(employees);
        }
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetEmployees());
            return Ok(employees);
        }
        [HttpGet("active/{active}")]
        public IActionResult GetEmployeesByActive(bool active)
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetEmployeesByActive(active));
            return Ok(employees);
        }
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employeeCreate)
        {
            if (employeeCreate == null)
            {
                return BadRequest(ModelState);
            }
            if(!_employeeRepository.CreateEmployee(_mapper.Map<Employee>(employeeCreate)))
            {
                return StatusCode(500, "Can't create employee");
            }
            return Ok("Create employee successfully");
        }
        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] EmployeeDto employeeUpdate)
        {
            if (employeeUpdate == null)
            {
                return BadRequest(ModelState);
            }
            if (!_employeeRepository.UpdateEmployee(_mapper.Map<Employee>(employeeUpdate)))
            {
                return StatusCode(500, "Can't update employee");
            }
            return Ok("Update employee successfully");
        }
        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            if (!_employeeRepository.DeleteEmployee(employeeId))
            {
                return StatusCode(500, "Can't delete employee");
            }
            return Ok("Delete employee successfully");
        }

    }
}
