using AutoMapper;
using HumanManagement.Data.Repository;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models.Dto;
using HumanManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController: ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetDepartments()
        {
            var departments = _mapper.Map<List<DepartmentDto>>(_departmentRepository.GetDepartments());
            return Ok(departments);
        }
        [HttpGet("{departmentId}")]
        public IActionResult GetDepartment(int departmentId)
        {
            var department = _mapper.Map<DepartmentDto>(_departmentRepository.GetDepartmentById(departmentId));
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);

        }
        [HttpGet("active/{active}")]
        public IActionResult GetDepartmentByActive(bool active)
        {
            var departments = _mapper.Map<List<DepartmentDto>>(_departmentRepository.GetDepartmentsByActive(active));
            return Ok(departments);
        }
        [HttpPost]
        public IActionResult CreateDepartment([FromBody] DepartmentDto departmentCreate)
        {
            if (departmentCreate == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_departmentRepository.CreateDepartment(_mapper.Map<Department>(departmentCreate)))
            {
                ModelState.AddModelError("", "Can't create department");
                return StatusCode(500, ModelState);
            }
            return Ok("Create department successfully");
        }
        [HttpPut]
        public IActionResult UpdateDepartment([FromBody] DepartmentDto departmentUpdate)
        {
            if (departmentUpdate == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_departmentRepository.UpdateDepartment(_mapper.Map<Department>(departmentUpdate)))
            {
                ModelState.AddModelError("", "Can't update department");
                return StatusCode(500, ModelState);
            }
            return Ok("Update department successfully");
        }
        [HttpDelete("{departmentId}")]
        public IActionResult DeleteDepartment(int departmentId)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_departmentRepository.DeleteDepartment(departmentId))
            {
                ModelState.AddModelError("", "Can't delete department");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete department successfully");
        }



    }
}
