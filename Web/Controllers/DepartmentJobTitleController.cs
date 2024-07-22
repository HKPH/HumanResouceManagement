using AutoMapper;
using HumanManagement.Data.Repository;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public IActionResult GetBenefitsByContract(int departmentId)
        {
            var benefits = _departmentJobTitleRepository.GetJobtitlesByDepartment(departmentId);
            return Ok(benefits);
        }
        [HttpPost]
        public IActionResult CreateDepartmentJobTitle([FromBody] DepartmentJobTitleDto djCreate)
        {
            if (djCreate == null)
                return BadRequest(ModelState);
            if (!_departmentJobTitleRepository.CreateDepartmentJobTitle(_mapper.Map<DepartmentJobTitle>(djCreate)))
            {
                ModelState.AddModelError("", "Can't create");
                return StatusCode(500, ModelState);
            }
            return Ok("Create successfully");
        }
        [HttpPut]
        public IActionResult UpdateDepartmentJobTitle([FromBody] DepartmentJobTitleDto djUpdate)
        {
            if (djUpdate == null)
                return BadRequest(ModelState);
            if (!_departmentJobTitleRepository.UpdateDepartmentJobTitle(_mapper.Map<DepartmentJobTitle>(djUpdate)))
            {
                ModelState.AddModelError("", "Can't update");
                return StatusCode(500, ModelState);
            }
            return Ok("Update successfully");
        }
        [HttpDelete]
        public IActionResult DeleteDepartmentJobTitle([FromBody] DepartmentJobTitleDto djDelete)
        {
            if (djDelete == null)
                return BadRequest(ModelState);
            if (!_departmentJobTitleRepository.DeleteDepartmentJobTitle(djDelete.DepartmentId, djDelete.JobTitleId))
            {
                ModelState.AddModelError("", "Can't delete");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete successfully");
        }


    }
}
