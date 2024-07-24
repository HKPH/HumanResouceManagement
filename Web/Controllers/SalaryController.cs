using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController:ControllerBase
    {
        private readonly ISalaryRepository _salaryRepository;
        private readonly IMapper _mapper;
        public SalaryController(ISalaryRepository salaryRepository, IMapper mapper)
        {
            _salaryRepository = salaryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetSalaries()
        {
            var salaries=_mapper.Map<List<SalaryDto>>(_salaryRepository.GetSalaries());
            return Ok(salaries);
        }
        [HttpGet("unused")]
        public IActionResult GetSalariesNotUsing()
        {
            var salaries = _mapper.Map<List<SalaryDto>>(_salaryRepository.GetSalariesNotUsing());
            return Ok(salaries);
        }
        [HttpPost]
        public IActionResult CreateSalary([FromBody]SalaryDto salaryCreate)
        {
            if (salaryCreate == null)
                return BadRequest(ModelState);
            if(!_salaryRepository.CreateSalary(_mapper.Map<Salary>(salaryCreate)))
            {
                return StatusCode(500, "Can't create salary");
            }
            return Ok("Create salary successfully");
        }
        [HttpPut]
        public IActionResult UpdateSalary([FromBody] SalaryDto salaryUpdate)
        {
            if (salaryUpdate == null)
                return BadRequest(ModelState);
            if (!_salaryRepository.UpdateSalary(_mapper.Map<Salary>(salaryUpdate)))
            {
                return StatusCode(500, "Can't update salary");
            }
            return Ok("Update salary successfully");
        }

    }
}
