using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTitleController : ControllerBase
    {
        private readonly IJobTitleRepository _jobTitleRepository;
        private readonly IMapper _mapper;

        public JobTitleController(IJobTitleRepository jobTitleRepository, IMapper mapper)
        {
            _jobTitleRepository = jobTitleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetJobTitles()
        {
            var jobTitles = _mapper.Map<List<JobTitleDto>>(await _jobTitleRepository.GetJobTitlesAsync());
            return Ok(jobTitles);
        }

        [HttpGet("{jobTitleId}")]
        public async Task<IActionResult> GetJobTitle(int jobTitleId)
        {
            var jobTitle = _mapper.Map<JobTitleDto>(await _jobTitleRepository.GetJobTitleByIdAsync(jobTitleId));
            if (jobTitle == null)
                return NotFound();
            return Ok(jobTitle);
        }

        [HttpGet("active/{active}")]
        public async Task<IActionResult> GetJobTitleByActive(bool active)
        {
            var jobTitles = _mapper.Map<List<JobTitleDto>>(await _jobTitleRepository.GetJobTitlesByActiveAsync(active));
            return Ok(jobTitles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobTitle([FromBody] JobTitleDto jobTitleDto)
        {
            if (jobTitleDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdJobTitle = await _jobTitleRepository.CreateJobTitleAsync(_mapper.Map<JobTitle>(jobTitleDto));
            var createdJobTitleDto = _mapper.Map<JobTitleDto>(createdJobTitle);

            if (createdJobTitle == null)
            {
                ModelState.AddModelError("", "Can't create job title");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction(
                nameof(GetJobTitle),
                new { jobTitleId = createdJobTitle.Id},
                createdJobTitleDto);
        }

        [HttpPut("{jobTitleId}")]
        public async Task<IActionResult> UpdateJobTitle(int jobTitleId, [FromBody] JobTitleDto jobTitleDto)
        {
            if (jobTitleDto == null)
                return BadRequest(ModelState);

            if (jobTitleId != jobTitleDto.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var jobTitle = _mapper.Map<JobTitle>(jobTitleDto);
            var updatedJobTitle = await _jobTitleRepository.UpdateJobTitleAsync(jobTitle);
            if (updatedJobTitle == null)
            {
                ModelState.AddModelError("", "Can't update job title");
                return StatusCode(500, ModelState);
            }

            return Ok("Update job title successfully");
        }

        [HttpDelete("{jobTitleId}")]
        public async Task<IActionResult> DeleteJobTitle(int jobTitleId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deletedJobTitle = await _jobTitleRepository.DeleteJobTitleAsync(jobTitleId);
            if (deletedJobTitle == null)
            {
                ModelState.AddModelError("", "Can't delete job title");
                return StatusCode(500, ModelState);
            }

            return Ok("Delete job title successfully");
        }
    }
}
