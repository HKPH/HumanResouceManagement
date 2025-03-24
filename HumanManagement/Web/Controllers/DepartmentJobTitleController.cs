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
    public class DepartmentJobTitleController : ControllerBase
    {
        private readonly IDepartmentJobTitleRepository _departmentJobTitleRepository;
        private readonly IMapper _mapper;

        public DepartmentJobTitleController(IDepartmentJobTitleRepository departmentJobTitleRepository, IMapper mapper)
        {
            _departmentJobTitleRepository = departmentJobTitleRepository;
            _mapper = mapper;
        }

        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetJobTitlesByDepartment(int departmentId)
        {
            var jobTitles = await _departmentJobTitleRepository.GetJobTitlesByDepartmentAsync(departmentId);
            var jobTitleDtos = _mapper.Map<List<JobTitleDto>>(jobTitles);
            return Ok(jobTitleDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartmentJobTitle([FromBody] DepartmentJobTitleDto djCreate)
        {
            if (djCreate == null)
                return BadRequest("Invalid data.");

            var departmentJobTitle = _mapper.Map<DepartmentJobTitle>(djCreate);
            var createdDepartmentJobTitle = await _departmentJobTitleRepository.CreateDepartmentJobTitleAsync(departmentJobTitle);

            if (createdDepartmentJobTitle == null)
            {
                ModelState.AddModelError("", "Can't create department job title");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201, "Create successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDepartmentJobTitle([FromBody] DepartmentJobTitleDto djUpdate)
        {
            if (djUpdate == null)
                return BadRequest("Invalid data.");

            var departmentJobTitle = _mapper.Map<DepartmentJobTitle>(djUpdate);
            var result = await _departmentJobTitleRepository.UpdateDepartmentJobTitleAsync(departmentJobTitle);

            if (result == null)
            {
                ModelState.AddModelError("", "Can't update department job title");
                return StatusCode(500, ModelState);
            }

            return Ok("Update successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDepartmentJobTitle([FromBody] DepartmentJobTitleDto djDelete)
        {
            if (djDelete == null)
                return BadRequest("Invalid data.");

            var result = await _departmentJobTitleRepository.DeleteDepartmentJobTitleAsync(djDelete.DepartmentId, djDelete.JobTitleId);

            if (result == null)
            {
                ModelState.AddModelError("", "Can't delete department job title");
                return StatusCode(500, ModelState);
            }

            return Ok("Delete successfully");
        }
    }
}
