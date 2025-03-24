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
        public async Task<IActionResult> GetEmployeesDOB([FromQuery] int days)
        {
            if (days < 0)
            {
                return BadRequest("Days cannot be negative.");
            }

            var employees = await _employeeRepository.GetEmployeesBirthdayAsync(days);
            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployeeById(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            var employeeDto = _mapper.Map<List<EmployeeDto>>(employee);
            return Ok(employeeDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }

        [HttpGet("active/{active}")]
        public async Task<IActionResult> GetEmployeesByActive(bool active)
        {
            var employees = await _employeeRepository.GetEmployeesByActiveAsync(active);
            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest(ModelState);
            }

            var employee = _mapper.Map<Employee>(employeeDto);
            var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employee);

            if (createdEmployee == null)
            {
                return StatusCode(500, "Can't create");
            }
            
            var createdEmployeeDto = _mapper.Map<EmployeeDto>(createdEmployee);
            return CreatedAtAction(
                nameof(GetEmployeeById),
                new { employeeId = createdEmployee.Id },
                createdEmployeeDto);
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, [FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);

            }

            if(employeeId != employeeDto.Id)
            {
                return BadRequest(ModelState);
            }    

            var employee = _mapper.Map<Employee>(employeeDto);
            var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(employee);

            if (updatedEmployee == null)
            {
                return StatusCode(500, "Can't update");
            }

            return Ok("Update successfully");
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var deletedEmployee = await _employeeRepository.DeleteEmployeeAsync(employeeId);

            if (deletedEmployee == null)
            {
                return StatusCode(500, "Can't delete");
            }

            return Ok("Delete successfully");
        }
    }
}
