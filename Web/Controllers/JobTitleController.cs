using AutoMapper;
using HumanManagement.Data.Repository;
using HumanManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTitleController:ControllerBase
    {
        private readonly IJobTitleRepository _jobTitleRepository;
        private readonly IMapper _mapper;
        public JobTitleController(IJobTitleRepository jobTitleRepository, IMapper mapper)
        {
            _jobTitleRepository = jobTitleRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetJobTitles()
        {
            var jobTitles= _jobTitleRepository.GetJobTitles();
            return Ok(jobTitles);
        }
        [HttpGet("{jobTitleId}")]
        public IActionResult GetJobTitleById(int jobTitleId)
        {
            var jobTitle = _jobTitleRepository.GetJobTitleById(jobTitleId);
            return Ok(jobTitle);
        }
        [HttpGet("active/{active}")]
        public IActionResult GetJobTitleByActive(bool active)
        {
            var jobTitles = _jobTitleRepository.GetJobTitleByActive(active);
            return Ok(jobTitles);
        }
        [HttpPost]
        public IActionResult CreateJobTitle([FromBody] JobTitle jobTitle)
        {
            if(jobTitle == null) 
                return BadRequest(ModelState);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!_jobTitleRepository.CreateJobTitle(jobTitle))
            {
                ModelState.AddModelError("", "Can't create job title");
                return StatusCode(500,ModelState);
            }
            return Ok("Create job title successfully");
        }
        [HttpPut]
        public IActionResult UpdateJobTitle([FromBody] JobTitle jobTitle)
        {
            if (jobTitle == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_jobTitleRepository.UpdateJobTitle(jobTitle))
            {
                ModelState.AddModelError("", "Can't update job title");
                return StatusCode(500, ModelState);
            }
            return Ok("Update job title successfully");
        }
        [HttpDelete]
        public IActionResult DeleteJobTitle([FromBody] JobTitle jobTitle)
        {
            if (jobTitle == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_jobTitleRepository.DeleteJobTitle(jobTitle))
            {
                ModelState.AddModelError("", "Can't delete job title");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete job title successfully");
        }
    }
}
