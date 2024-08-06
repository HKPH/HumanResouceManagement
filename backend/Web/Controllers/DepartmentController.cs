using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models.Dto;
using HumanManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = _mapper.Map<List<DepartmentDto>>(await _departmentRepository.GetDepartmentsAsync());
            return Ok(departments);
        }

        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetDepartment(int departmentId)
        {
            var department = _mapper.Map<DepartmentDto>(await _departmentRepository.GetDepartmentByIdAsync(departmentId));
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpGet("active/{active}")]
        public async Task<IActionResult> GetDepartmentByActive(bool active)
        {
            var departments = _mapper.Map<List<DepartmentDto>>(await _departmentRepository.GetDepartmentsByActiveAsync(active));
            return Ok(departments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            if (departmentDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var department = _mapper.Map<Department>(departmentDto);
            var createdDepartment = await _departmentRepository.CreateDepartmentAsync(department);

            if (createdDepartment == null)
            {
                ModelState.AddModelError("", "Can't create department");
                return StatusCode(500, ModelState);
            }

            var createdDepartmentDto = _mapper.Map<DepartmentDto>(createdDepartment);
            return CreatedAtAction(
                nameof(GetDepartment),
                new { departmentId = createdDepartment.Id },
                createdDepartmentDto); 

        }

        [HttpPut("{departmentId}")]
        public async Task<IActionResult> UpdateDepartment(int departmentId, [FromBody] DepartmentDto departmentDto)
        {
            if (departmentDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(departmentId != departmentDto.Id)
                return BadRequest(ModelState);

            var department = _mapper.Map<Department>(departmentDto);
            var updatedDepartment = await _departmentRepository.UpdateDepartmentAsync(department);

            if (updatedDepartment == null)
            {
                ModelState.AddModelError("", "Can't update department");
                return StatusCode(500, ModelState);
            }

            return Ok("Update department successfully");
        }

        [HttpDelete("{departmentId}")]
        public async Task<IActionResult> DeleteDepartment(int departmentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deletedDepartment = await _departmentRepository.DeleteDepartmentAsync(departmentId);

            if (deletedDepartment == null)
            {
                ModelState.AddModelError("", "Can't delete department");
                return StatusCode(500, ModelState);
            }

            return Ok("Delete department successfully");
        }
    }
}
