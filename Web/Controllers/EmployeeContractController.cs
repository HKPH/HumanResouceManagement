using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeContractController : ControllerBase
    {
        private readonly IEmployeeContractRepository _employeeContractRepository;
        private readonly IMapper _mapper;

        public EmployeeContractController(IEmployeeContractRepository employeeContractRepository, IMapper mapper)
        {
            _employeeContractRepository = employeeContractRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeContracts()
        {
            var employeeContracts = await _employeeContractRepository.GetEmployeeContractsAsync();
            return Ok(employeeContracts);
        }

        [HttpGet("{employeeContractId}")]
        public async Task<IActionResult> GetEmployeeContract(int employeeContractId)
        {
            var employeeContract = await _employeeContractRepository.GetEmployeeContractByIdAsync(employeeContractId);
            if (employeeContract == null)
            {
                return NotFound();
            }
            return Ok(employeeContract);
        }

        [HttpGet("active/{active}")]
        public async Task<IActionResult> GetEmployeeContractsByActive(bool active)
        {
            var employeeContracts = await _employeeContractRepository.GetEmployeeContractsByActiveAsync(active);
            return Ok(employeeContracts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeContract([FromBody] EmployeeContractDto employeeContractDto)
        {
            if (employeeContractDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeContract = _mapper.Map<EmployeeContract>(employeeContractDto);
            var createdEmployeeContract = await _employeeContractRepository.CreateEmployeeContractAsync(employeeContract);

            if (createdEmployeeContract == null)
            {
                ModelState.AddModelError("", "Can't create");
                return StatusCode(500, ModelState);
            }

            var createdEmployeeContractDto=_mapper.Map<EmployeeContractDto>(createdEmployeeContract);
            return CreatedAtAction(
                nameof(GetEmployeeContract),
                new { employeeContractId = employeeContract.Id },
                createdEmployeeContractDto);
        }

        [HttpPut("{employeeContractId}")]
        public async Task<IActionResult> UpdateEmployeeContract(int employeeContractId, [FromBody] EmployeeContractDto employeeContractDto)
        {
            if (employeeContractDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(employeeContractDto.Id != employeeContractId)
                return BadRequest(ModelState);

            var employeeContract = _mapper.Map<EmployeeContract>(employeeContractDto);
            var updatedEmployeeContract = await _employeeContractRepository.UpdateEmployeeContractAsync(employeeContract);
            if (updatedEmployeeContract == null)
            {
                ModelState.AddModelError("", "Can't update");
                return StatusCode(500, ModelState);
            }

            return Ok("Update successfully");
        }

        [HttpDelete("{employeeContractId}")]
        public async Task<IActionResult> DeleteEmployeeContract(int employeeContractId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deletedEmployeeContract = await _employeeContractRepository.DeleteEmployeeContractAsync(employeeContractId);
            if (deletedEmployeeContract == null)
            {
                ModelState.AddModelError("", "Can't delete");
                return StatusCode(500, ModelState);
            }

            return Ok("Delete successfully");
        }
    }
}
