using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeContractController: ControllerBase
    {
        private readonly IEmployeeContractRepository _employeeContractRepository;
        private readonly IMapper _mapper;
        public EmployeeContractController(IEmployeeContractRepository employeeContractRepository, IMapper mapper)
        {
            _employeeContractRepository = employeeContractRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetEmployeeContracts()
        {
            var employeeContract = _employeeContractRepository.GetEmployeeContracts();
            return Ok(employeeContract);
        }
        [HttpGet("{employeeContractId}")]
        public IActionResult GetEmployeeContract(int employeeContractId)
        {
            var employeeContract = _employeeContractRepository.GetEmployeeContractById(employeeContractId);
            if (employeeContract == null)
            {
                return NotFound();
            }
            return Ok(employeeContract);

        }
        [HttpGet("active/{active}")]
        public IActionResult GetEmployeeContractByActive(bool active)
        {
            var contractTypes = _employeeContractRepository.GetEmployeeContractsByActive(active);
            return Ok(contractTypes);
        }
        [HttpPost]
        public IActionResult CreateEmployeeContract([FromBody] EmployeeContract employeeContract)
        {
            if (employeeContract == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_employeeContractRepository.CreateEmployeeContract(employeeContract))
            {
                ModelState.AddModelError("", "Can't create employee contract");
                return StatusCode(500, ModelState);
            }
            return Ok("Create employee contract successfully");
        }
        [HttpPut]
        public IActionResult UpdateEmployeeContract([FromBody] EmployeeContract employeeContract)
        {
            if (employeeContract == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_employeeContractRepository.UpdateEmployeeContract(employeeContract))
            {
                ModelState.AddModelError("", "Can't update employee contract");
                return StatusCode(500, ModelState);
            }
            return Ok("Update employee contract successfully");
        }
        [HttpDelete("{employeeContractId}")]
        public IActionResult DeleteEmployeeContract(int employeeContractId)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_employeeContractRepository.DeleteEmployeeContract(employeeContractId))
            {
                ModelState.AddModelError("", "Can't delete employee contract");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete employee contract successfully");
        }
    }
}
