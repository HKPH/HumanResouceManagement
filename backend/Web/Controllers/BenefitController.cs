using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenefitController : ControllerBase
    {
        private readonly IBenefitRepository _benefitRepository;
        private readonly IMapper _mapper;

        public BenefitController(IBenefitRepository benefitRepository, IMapper mapper)
        {
            _benefitRepository = benefitRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBenefits()
        {
            var benefits = _mapper.Map<List<BenefitDto>>(await _benefitRepository.GetBenefitsAsync());
            return Ok(benefits);
        }

        [HttpGet("{benefitId}")]
        public async Task<IActionResult> GetBenefit(int benefitId)
        {
            var benefit = _mapper.Map<BenefitDto>(await _benefitRepository.GetBenefitByIdAsync(benefitId));
            return benefit == null ? NotFound() : (IActionResult)Ok(benefit);
        }

        [HttpGet("active/{active}")]
        public async Task<IActionResult> GetBenefitByActive(bool active)
        {
            var benefits = _mapper.Map<List<BenefitDto>>(await _benefitRepository.GetBenefitsByActiveAsync(active));
            return Ok(benefits);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBenefit([FromBody] BenefitDto benefitDto)
        {
            if (benefitDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var benefit = _mapper.Map<Benefit>(benefitDto);
            var createdBenefit = await _benefitRepository.CreateBenefitAsync(benefit);

            if (createdBenefit == null)
            {
                ModelState.AddModelError("", "Can't create benefit");
                return StatusCode(500, ModelState);
            }

            var createdBenefitDto = _mapper.Map<BenefitDto>(createdBenefit);

            return CreatedAtAction(
                actionName: nameof(GetBenefit),
                routeValues: new { benefitId = createdBenefitDto.Id }, 
                value: createdBenefitDto);
        }

        [HttpPut("{benefitId}")]
        public async Task<IActionResult> UpdateBenefit(int benefitId, [FromBody] BenefitDto benefitDto)
        {
            if (benefitDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(benefitId != benefitDto.Id)
                return BadRequest(ModelState);

            var benefit = _mapper.Map<Benefit>(benefitDto);
            var updatedBenefit = await _benefitRepository.UpdateBenefitAsync(benefit);

            if (updatedBenefit == null)
            {
                ModelState.AddModelError("", "Can't update benefit");
                return StatusCode(500, ModelState);
            }

            return Ok("Update benefit successfully");
        }

        [HttpDelete("{benefitId}")]
        public async Task<IActionResult> DeleteBenefit(int benefitId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deletedBenefit = await _benefitRepository.DeleteBenefitAsync(benefitId);

            if (deletedBenefit == null)
            {
                ModelState.AddModelError("", "Can't delete benefit");
                return StatusCode(500, ModelState);
            }

            return Ok("Delete benefit successfully");
        }
    }
}
