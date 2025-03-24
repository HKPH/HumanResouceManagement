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
    public class EmployeeBenefitController : ControllerBase
    {
        private readonly IEmployeeBenefitRepository _employeeBenefitRepository;
        private readonly IMapper _mapper;

        public EmployeeBenefitController(IEmployeeBenefitRepository employeeBenefitRepository, IMapper mapper)
        {
            _employeeBenefitRepository = employeeBenefitRepository;
            _mapper = mapper;
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetBenefitsByEmployee(int employeeId)
        {
            var benefits = await _employeeBenefitRepository.GetBenefitsByEmployeeAsync(employeeId);
            var benefitDtos = _mapper.Map<List<BenefitDto>>(benefits);
            return Ok(benefitDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeBenefit([FromBody] EmployeeBenefitDto ebCreate)
        {
            if (ebCreate == null)
                return BadRequest(ModelState);

            var employeeBenefit = _mapper.Map<EmployeeBenefit>(ebCreate);
            var createdEmployeeBenefit = await _employeeBenefitRepository.CreateEmployeeBenefitAsync(employeeBenefit);

            if (createdEmployeeBenefit == null)
            {
                ModelState.AddModelError("", "Can't create");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201, "Create successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeBenefit([FromBody] EmployeeBenefitDto ebUpdate)
        {
            if (ebUpdate == null)
                return BadRequest(ModelState);

            var employeeBenefit = _mapper.Map<EmployeeBenefit>(ebUpdate);
            var updatedEmployeeBenefit = await _employeeBenefitRepository.UpdateEmployeeBenefitAsync(employeeBenefit);

            if (updatedEmployeeBenefit == null)
            {
                ModelState.AddModelError("", "Can't update");
                return StatusCode(500, ModelState);
            }

            return Ok("Update successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployeeBenefit([FromBody] EmployeeBenefitDto ebDelete)
        {
            if (ebDelete == null)
                return BadRequest(ModelState);

            var deletedEmployeeBenefit = await _employeeBenefitRepository.DeleteEmployeeBenefitAsync(ebDelete.EmployeeId, ebDelete.BenefitId);

            if (deletedEmployeeBenefit == null)
            {
                ModelState.AddModelError("", "Can't delete");
                return StatusCode(500, ModelState);
            }

            return Ok("Delete successfully");
        }
    }
}
