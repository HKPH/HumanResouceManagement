using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
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
            var jobTitles= _mapper.Map<List<JobTitleDto>>(_jobTitleRepository.GetJobTitles());
            return Ok(jobTitles);
        }
        [HttpGet("{jobTitleId}")]
        public IActionResult GetJobTitle(int jobTitleId)
        {
            var jobTitle = _mapper.Map<JobTitleDto>(_jobTitleRepository.GetJobTitleById(jobTitleId));
            return Ok(jobTitle);
        }
        [HttpGet("active/{active}")]
        public IActionResult GetJobTitleByActive(bool active)
        {
            var jobTitles = _mapper.Map<List<JobTitleDto>>(_jobTitleRepository.GetJobTitlesByActive(active));
            return Ok(jobTitles);
        }
        [HttpPost]
        public IActionResult CreateJobTitle([FromBody] JobTitleDto jobTitle)
        {
            if(jobTitle == null) 
                return BadRequest(ModelState);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_jobTitleRepository.CreateJobTitle(_mapper.Map<JobTitle>(jobTitle)))
            {
                ModelState.AddModelError("", "Can't create job title");
                return StatusCode(500,ModelState);
            }
            return Ok("Create job title successfully");
        }
        [HttpPut("{jobTitleId}")]
        public IActionResult UpdateJobTitle(int jobTitleId,[FromBody] JobTitleDto jobTitle)
        {

            if (jobTitle == null)
                return BadRequest(ModelState);
            if(jobTitleId!=jobTitle.Id)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_jobTitleRepository.UpdateJobTitle(_mapper.Map<JobTitle>(jobTitle)))
            {
                ModelState.AddModelError("", "Can't update job title");
                return StatusCode(500, ModelState);
            }
            return Ok("Update job title successfully");
        }
        [HttpDelete("{jobTitleId}")]
        public IActionResult DeleteJobTitle(int jobTitleId)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_jobTitleRepository.DeleteJobTitle(jobTitleId))
            {
                ModelState.AddModelError("", "Can't delete job title");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete job title successfully");
        }
    }
}
