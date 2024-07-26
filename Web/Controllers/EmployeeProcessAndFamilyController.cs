using AutoMapper;
using HumanManagement.Data.Repository;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models.Dto;
using HumanManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProcessAndFamilyController: ControllerBase
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
        public IActionResult GetEmployeesProcessAndFamily(int employeeId)
        {
            var processes=_mapper.Map<List<EmployeeProcessDto>>(_employeeProcessRepository.GetEmployeeProcessesByEmployeeId(employeeId));
            var families= _mapper.Map<List<EmployeeFamilyDto>>(_employeeFamilyRepository.GetEmployeeFamiliesByEmployeeId(employeeId));
            if (processes == null || families == null)
            {
                return NotFound("Employee processes or families not found.");
            }

            var result = new
            {
                Processes = _mapper.Map<List<EmployeeProcessDto>>(processes),
                Families = _mapper.Map<List<EmployeeFamilyDto>>(families)
            };

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateEmployeesProcessAndFamily([FromBody] EmployeeProcessAndFamilyDto epfDto)
        {
            var epCreate = epfDto.EmployeeProcess;
            var efCreate = epfDto.EmployeeFamily;
            if (epCreate == null || efCreate == null)
            {
                return BadRequest(ModelState);
            }
            
            if (!_employeeFamilyRepository.CreateEmployeeFamily(_mapper.Map<EmployeeFamily>(efCreate)) &&
                !_employeeProcessRepository.CreateEmployeeProcess(_mapper.Map<EmployeeProcess>(epCreate)))
            {
                return StatusCode(500, "Can't create");
            }
            return Ok("Create employee successfully");
        }
        [HttpPut]
        public IActionResult UpdateEmployeesProcessAndFamily([FromBody] EmployeeProcessAndFamilyDto epfDto)
        {
            var epUpdate = epfDto.EmployeeProcess;
            var efUpdate = epfDto.EmployeeFamily;
            if (epUpdate == null && efUpdate == null)
            {
                return BadRequest("Both EmployeeProcessDto and EmployeeFamilyDto cannot be null.");
            }

            bool isProcessUpdated = true;
            bool isFamilyUpdated = true;

            if (epUpdate != null)
            {
                var employeeProcess = _mapper.Map<EmployeeProcess>(epUpdate);
                isProcessUpdated = _employeeProcessRepository.UpdateEmployeeProcess(employeeProcess);
                if (!isProcessUpdated)
                {
                    return StatusCode(500, "Can't update employee process.");
                }
            }

            if (efUpdate != null)
            {
                var employeeFamily = _mapper.Map<EmployeeFamily>(efUpdate);
                isFamilyUpdated = _employeeFamilyRepository.UpdateEmployeeFamily(employeeFamily);
                if (!isFamilyUpdated)
                {
                    return StatusCode(500, "Can't update employee family.");
                }
            }

            if (isProcessUpdated && isFamilyUpdated)
            {
                return Ok("Update employee process and family successfully.");
            }
            else if (isProcessUpdated)
            {
                return Ok("Update employee process successfully");
            }
            else if (isFamilyUpdated)
            {
                return Ok("Update employee family successfully");
            }

            return StatusCode(500, "Failed to update employee process and family.");
        }

        [HttpDelete]
        public IActionResult DeleteEmployee([FromQuery]int epId, [FromQuery] int efId)
        {
            if (!_employeeFamilyRepository.DeleteEmployeeFamily(efId)||!_employeeProcessRepository.DeleteEmployeeProcess(epId))
            {
                return StatusCode(500, "Can't delete ");
            }
            return Ok("Delete successfully");
        }

    }
}
