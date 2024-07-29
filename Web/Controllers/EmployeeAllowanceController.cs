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
    public class EmployeeAllowanceController : ControllerBase
    {
        private readonly IEmployeeAllowanceRepository _employeeAllowanceRepository;
        private readonly IMapper _mapper;

        public EmployeeAllowanceController(IEmployeeAllowanceRepository employeeAllowanceRepository, IMapper mapper)
        {
            _employeeAllowanceRepository = employeeAllowanceRepository;
            _mapper = mapper;
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetAllowancesByEmployee(int employeeId)
        {
            var allowances = _mapper.Map<List<AllowanceDto>>(await _employeeAllowanceRepository.GetAllowancesByEmployeeAsync(employeeId));
            return Ok(allowances);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAllowance([FromBody] EmployeeAllowanceDto eaCreate)
        {
            if (eaCreate == null)
                return BadRequest(ModelState);

            var employeeAllowance = _mapper.Map<EmployeeAllowance>(eaCreate);

            var createdAllowance = await _employeeAllowanceRepository.CreateEmployeeAllowanceAsync(employeeAllowance);

            if (createdAllowance == null)
            {
                ModelState.AddModelError("", "Can't create");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201, "Create successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeAllowance([FromBody] EmployeeAllowanceDto eaUpdate)
        {
            if (eaUpdate == null)
                return BadRequest(ModelState);

            var employeeAllowance = _mapper.Map<EmployeeAllowance>(eaUpdate);

            var updatedAllowance = await _employeeAllowanceRepository.UpdateEmployeeAllowanceAsync(employeeAllowance);

            if (updatedAllowance == null)
            {
                ModelState.AddModelError("", "Can't update");
                return StatusCode(500, ModelState);
            }

            return Ok("Update successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployeeAllowance([FromBody] EmployeeAllowanceDto eaDelete)
        {
            if (eaDelete == null)
                return BadRequest(ModelState);

            var deletedAllowance = await _employeeAllowanceRepository.DeleteEmployeeAllowanceAsync(eaDelete.EmployeeId, eaDelete.AllowanceId);

            if (deletedAllowance == null)
            {
                ModelState.AddModelError("", "Can't delete");
                return StatusCode(500, ModelState);
            }

            return Ok("Delete successfully");
        }
    }
}
