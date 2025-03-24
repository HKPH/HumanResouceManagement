using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResignationController : ControllerBase
    {
        private readonly IResignationRepository _resignationRepository;
        private readonly IMapper _mapper;

        public ResignationController(IResignationRepository resignationRepository, IMapper mapper)
        {
            _resignationRepository = resignationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetResignations()
        {
            var resignations = await _resignationRepository.GetResignationsAsync();
            if (resignations == null)
                return NotFound(ModelState);

            var resignationDtos = _mapper.Map<List<ResignationDto>>(resignations);
            return Ok(resignationDtos);
        }
        [HttpGet("{resignationId}")]
        public async Task<IActionResult> GetResignationById(int resignationId)
        {
            var resignation = await _resignationRepository.GetResignationByIdAsync(resignationId);
            if (resignation == null)
                return NotFound(ModelState);

            var resignationDto = _mapper.Map<ResignationDto>(resignation);
            return Ok(resignationDto);
        }

        [HttpGet("accepted/{accepted}")]
        public async Task<IActionResult> GetResignationsByAccepted(bool accepted)
        {
            var resignations = await _resignationRepository.GetResignationsByAcceptedAsync(accepted);
            if (resignations == null)
                return NotFound(ModelState);

            var resignationDtos = _mapper.Map<List<ResignationDto>>(resignations);
            return Ok(resignationDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateResignation([FromBody] ResignationDto resignationDto)
        {
            if (resignationDto == null)
                return BadRequest(ModelState);

            var resignation = _mapper.Map<Resignation>(resignationDto);
            var createdResignation = await _resignationRepository.CreateResignationAsync(resignation);
            var createdResignationDto = _mapper.Map<ResignationDto>(createdResignation);

            if (createdResignation == null)
                return StatusCode(500, "Can't create resignation.");

            return CreatedAtAction(
                nameof(GetResignationById),
                new { resignationId = createdResignation.Id },
                createdResignationDto);
        }

        [HttpPut("{resignationId}")]
        public async Task<IActionResult> UpdateResignation(int resignationId, [FromBody] ResignationDto resignationDto)
        {
            if (resignationDto == null || resignationDto.Id != resignationId)
                return BadRequest(ModelState);

            var resignation = _mapper.Map<Resignation>(resignationDto);
            var updatedResignation = await _resignationRepository.UpdateResignationAsync(resignation);

            if (updatedResignation == null)
                return StatusCode(500, "Can't update");

            return Ok("Update successfully");
        }

        [HttpDelete("{resignationId}")]
        public async Task<IActionResult> DeleteResignation(int resignationId)
        {
            var deletedResignation = await _resignationRepository.DeleteResignationAsync(resignationId);

            if (deletedResignation == null)
                return StatusCode(500, "Can't delete");

            return Ok("Delete successfully");
        }
    }
}
