using AutoMapper;
using HumanManagement.Models.Dto;
using HumanManagement.Models;
using Microsoft.AspNetCore.Mvc;
using HumanManagement.Data.Repository.Interface;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCareController : ControllerBase
    {
        private readonly IHealthCareRepository _healthCareRepository;
        private readonly IMapper _mapper;

        public HealthCareController(IHealthCareRepository healthCareRepository, IMapper mapper)
        {
            _healthCareRepository = healthCareRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHealthCares()
        {
            var healthCares = _mapper.Map<List<HealthCareDto>>(await _healthCareRepository.GetHealthCaresAsync());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(healthCares);
        }

        [HttpGet("{healthCareId}")]
        public async Task<IActionResult> GetHealthCare(int healthCareId)
        {
            var healthCare = _mapper.Map<HealthCareDto>(await _healthCareRepository.GetHealthCareByIdAsync(healthCareId));
            if (healthCare == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(healthCare);
        }

        [HttpGet("active/{active}")]
        public async Task<IActionResult> GetHealthCareByActive(bool active)
        {
            var healthCares = _mapper.Map<List<HealthCareDto>>(await _healthCareRepository.GetHealthCaresByActiveAsync(active));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(healthCares);
        }

        [HttpPost]
        public async Task<IActionResult> PostHealthCare([FromBody] HealthCareDto healthCareDto)
        {
            if (healthCareDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            healthCareDto.Active = true;

            var createdHealthCare = await _healthCareRepository.CreateHealthCareAsync(_mapper.Map<HealthCare>(healthCareDto));
            var createdHealthCareDto = _mapper.Map<HealthCareDto>(createdHealthCare);

            if (createdHealthCare == null)
            {
                ModelState.AddModelError("", "Can't create health care");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction(
                nameof(GetHealthCare),
                new { healthCareId = createdHealthCare .Id },
                createdHealthCareDto);
        }

        [HttpPut("{healthCareId}")]
        public async Task<IActionResult> UpdateHealthCare(int healthCareId, [FromBody] HealthCareDto healthCareDto)
        {
            if (healthCareDto == null)
            {
                return BadRequest("HealthCare data is null.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (healthCareId != healthCareDto.Id)
                return BadRequest(ModelState);

            var updatedHealthCare = await _healthCareRepository.UpdateHealthCareAsync(_mapper.Map<HealthCare>(healthCareDto));
            if (updatedHealthCare == null)
            {
                ModelState.AddModelError("", "Can't update health care");
                return StatusCode(500, ModelState);
            }

            return Ok("Update health care successfully");
        }

        [HttpDelete("{healthCareId}")]
        public async Task<IActionResult> DeleteHealthCare(int healthCareId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deletedHealthCare = await _healthCareRepository.DeleteHealthCareAsync(healthCareId);
            if (deletedHealthCare == null)
            {
                ModelState.AddModelError("", "Can't delete health care");
                return StatusCode(500, ModelState);
            }

            return Ok("Delete health care successfully");
        }
    }
}
