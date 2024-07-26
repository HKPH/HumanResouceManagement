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
        public IActionResult GetBenefitsByEmployee(int employeeId)
        {
            var Benefits = _mapper.Map<List<BenefitDto>>(_employeeBenefitRepository.GetBenefitsByEmployee(employeeId));
            return Ok(Benefits);
        }
        [HttpPost]
        public IActionResult CreateEmployeeBenefit([FromBody] EmployeeBenefitDto eaCreate)
        {
            if (eaCreate == null)
                return BadRequest(ModelState);
            if (!_employeeBenefitRepository.CreateEmployeeBenefit(_mapper.Map<EmployeeBenefit>(eaCreate)))
            {
                ModelState.AddModelError("", "Can't create");
                return StatusCode(500, ModelState);
            }
            return Ok("Create successfully");
        }
        [HttpPut]
        public IActionResult UpdateEmployeeBenefit([FromBody] EmployeeBenefitDto ebUpdate)
        {
            if (ebUpdate == null)
                return BadRequest(ModelState);
            if (!_employeeBenefitRepository.UpdateEmployeeBenefit(_mapper.Map<EmployeeBenefit>(ebUpdate)))
            {
                ModelState.AddModelError("", "Can't update");
                return StatusCode(500, ModelState);
            }
            return Ok("Update successfully");
        }
        [HttpDelete]
        public IActionResult DeleteEmployeeBenefit([FromBody] EmployeeBenefitDto ebDelete)
        {
            if (ebDelete == null)
                return BadRequest(ModelState);
            if (!_employeeBenefitRepository.DeleteEmployeeBenefit(ebDelete.EmployeeId, ebDelete.BenefitId))
            {
                ModelState.AddModelError("", "Can't delete");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete successfully");
        }
    }
}
