using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;


namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProcessAndFamilyController : ControllerBase
    {
        private readonly IEmployeeProcessRepository _employeeProcessRepository;
        private readonly IEmployeeFamilyRepository _employeeFamilyRepository;
        private readonly IMapper _mapper;

        public EmployeeProcessAndFamilyController(IEmployeeProcessRepository employeeProcessRepository, IEmployeeFamilyRepository employeeFamilyRepository, IMapper mapper)
        {
            _employeeProcessRepository = employeeProcessRepository;
            _employeeFamilyRepository = employeeFamilyRepository;
            _mapper = mapper;
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployeesProcessAndFamily(int employeeId)
        {
            var processes = await _employeeProcessRepository.GetEmployeeProcessesByEmployeeIdAsync(employeeId);
            var families = await _employeeFamilyRepository.GetEmployeeFamiliesByEmployeeIdAsync(employeeId);

            if (processes == null && families == null)
            {
                return NotFound("Employee processes and families not found.");
            }

            var result = new
            {
                Processes = processes != null ? _mapper.Map<List<EmployeeProcessDto>>(processes) : null,
                Families = families != null ? _mapper.Map<List<EmployeeFamilyDto>>(families) : null
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeesProcessAndFamily([FromBody] EmployeeProcessAndFamilyDto epfDto)
        {
            var epCreate = epfDto.EmployeeProcess;
            var efCreate = epfDto.EmployeeFamily;

            if (epCreate == null && efCreate == null)
            {
                return BadRequest("Both EmployeeProcessDto and EmployeeFamilyDto cannot be null.");
            }

            if (epCreate != null)
            {
                var employeeProcess = _mapper.Map<EmployeeProcess>(epCreate);
                var createdProcess = await _employeeProcessRepository.CreateEmployeeProcessAsync(employeeProcess);
                if (createdProcess == null)
                {
                    return StatusCode(500, "Can't create employee process");
                }
            }

            if (efCreate != null)
            {
                var employeeFamily = _mapper.Map<EmployeeFamily>(efCreate);
                var createdFamily = await _employeeFamilyRepository.CreateEmployeeFamilyAsync(employeeFamily);
                if (createdFamily == null)
                {
                    return StatusCode(500, "Can't create employee family");
                }
            }

            return StatusCode(201,"Created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployeesProcessAndFamily([FromBody] EmployeeProcessAndFamilyDto epfDto)
        {
            var epUpdate = epfDto.EmployeeProcess;
            var efUpdate = epfDto.EmployeeFamily;

            if (epUpdate == null && efUpdate == null)
            {
                return BadRequest("Both EmployeeProcessDto and EmployeeFamilyDto cannot be null.");
            }

            if (epUpdate != null)
            {
                var employeeProcess = _mapper.Map<EmployeeProcess>(epUpdate);
                var updatedProcess = await _employeeProcessRepository.UpdateEmployeeProcessAsync(employeeProcess);
                if (updatedProcess == null)
                {
                    return StatusCode(500, "Can't update employee process");
                }
            }

            if (efUpdate != null)
            {
                var employeeFamily = _mapper.Map<EmployeeFamily>(efUpdate);
                var updatedFamily = await _employeeFamilyRepository.UpdateEmployeeFamilyAsync(employeeFamily);
                if (updatedFamily == null)
                {
                    return StatusCode(500, "Can't update employee family");
                }
            }

            return Ok("Updated successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee([FromQuery] int epId, [FromQuery] int efId)
        {
            var deletedProcess = await _employeeProcessRepository.DeleteEmployeeProcessAsync(epId);
            var deletedFamily = await _employeeFamilyRepository.DeleteEmployeeFamilyAsync(efId);

            if (deletedProcess == null && deletedFamily == null)
            {
                return StatusCode(500, "Can't delete");
            }

            return Ok("Deleted successfully");
        }
    }
}
