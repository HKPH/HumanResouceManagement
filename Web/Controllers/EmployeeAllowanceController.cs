using AutoMapper;
using HumanManagement.Data.Repository;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAllowanceController:ControllerBase
    {
        private readonly IEmployeeAllowanceRepository _employeeAllowanceRepository;
        private readonly IMapper _mapper;
        public EmployeeAllowanceController(IEmployeeAllowanceRepository employeeAllowanceRepository, IMapper mapper)
        {
            _employeeAllowanceRepository = employeeAllowanceRepository;
            _mapper = mapper;
        }
        [HttpGet("{employeeId}")]
        public IActionResult GetAllowancesByEmployee(int employeeId)
        {
            var allowances = _mapper.Map<List<AllowanceDto>>(_employeeAllowanceRepository.GetAllowancesByEmployee(employeeId));
            return Ok(allowances);
        }
        [HttpPost]
        public IActionResult CreateEmployeeAllowance([FromBody] EmployeeAllowanceDto eaCreate)
        {
            if (eaCreate == null)
                return BadRequest(ModelState);
            if (!_employeeAllowanceRepository.CreateEmployeeAllowance(_mapper.Map<EmployeeAllowance>(eaCreate)))
            {
                ModelState.AddModelError("", "Can't create");
                return StatusCode(500, ModelState);
            }
            return Ok("Create successfully");
        }
        [HttpPut]
        public IActionResult UpdateEmployeeAllowance([FromBody] EmployeeAllowanceDto eaUpdate)
        {
            if (eaUpdate == null)
                return BadRequest(ModelState);
            if (!_employeeAllowanceRepository.UpdateEmployeeAllowance(_mapper.Map<EmployeeAllowance>(eaUpdate)))
            {
                ModelState.AddModelError("", "Can't update");
                return StatusCode(500, ModelState);
            }
            return Ok("Update successfully");
        }
        [HttpDelete]
        public IActionResult DeleteEmployeeAllowance([FromBody] EmployeeAllowanceDto caDelete)
        {
            if (caDelete == null)
                return BadRequest(ModelState);
            if (!_employeeAllowanceRepository.DeleteEmployeeAllowance(caDelete.EmployeeId, caDelete.AllowanceId))
            {
                ModelState.AddModelError("", "Can't delete");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete successfully");
        }
    }
}
