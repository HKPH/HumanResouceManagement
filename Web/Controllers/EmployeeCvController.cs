using AutoMapper;
using HumanManagement.Data.Repository;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeCvController:ControllerBase
    {
        private readonly IEmployeeCvRepository _employeeCvRepository;
        private readonly IMapper _mapper;

        public EmployeeCvController(IEmployeeCvRepository employeeCvRepository,IMapper mapper)
        { 
            _employeeCvRepository = employeeCvRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetEmployeeCvs()
        {
            var employeeCvs=_mapper.Map<List<EmployeeCvDto>>(_employeeCvRepository.GetEmployeeCvs());
            return Ok(employeeCvs);

        }
        [HttpGet("{employeeCvId}")]
        public IActionResult GetEmployeeCv(int employeeCvId)
        {
            var employeeCv=_mapper.Map<EmployeeCvDto>(_employeeCvRepository.GetEmployeesCvById(employeeCvId));
            return Ok(employeeCv);
        }
        [HttpGet("active/{active}")]
        public IActionResult GetEmployeeCvByActive(bool active)
        {
            var employeeCv = _mapper.Map<List<EmployeeCvDto>>(_employeeCvRepository.GetEmployeesCvByActive(active));
            return Ok(employeeCv);
        }
        [HttpPost]
        public IActionResult CreateEmployeeCv([FromBody] EmployeeCv employeeCv)
        {
            if (employeeCv == null)
                return BadRequest(ModelState);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!_employeeCvRepository.CreateEmployeeCv(employeeCv))
            {
                ModelState.AddModelError("", "Can't create employeeCv");
                return StatusCode(500,ModelState);
            }
            return Ok("Create employee cv successfully");
        }
        [HttpPut]
        public IActionResult UpdateEmployeeCv([FromBody] EmployeeCv employeeCv)
        {
            if (employeeCv == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_employeeCvRepository.UpdateEmployeeCv(employeeCv))
            {
                ModelState.AddModelError("", "Can't update employeeCv");
                return StatusCode(500, ModelState);
            }
            return Ok("Update employee cv successfully");
        }
        [HttpDelete]
        public IActionResult DeleteEmployeeCv([FromBody] EmployeeCv employeeCv)
        {
            if (employeeCv == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_employeeCvRepository.DeleteEmployeeCv(employeeCv))
            {
                ModelState.AddModelError("", "Can't delete employeeCv");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete employee cv successfully");
        }


    }
}
