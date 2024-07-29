using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllowanceController : ControllerBase
    {
        private readonly IAllowanceRepository _allowanceRepository;
        private readonly IMapper _mapper;

        public AllowanceController(IAllowanceRepository allowanceRepository, IMapper mapper)
        {
            _allowanceRepository = allowanceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllowances()
        {
            var allowances = await _allowanceRepository.GetAllowancesAsync();
            var allowanceDtos = _mapper.Map<List<AllowanceDto>>(allowances);
            return Ok(allowanceDtos);
        }

        [HttpGet("{allowanceId}")]
        public async Task<IActionResult> GetAllowance(int allowanceId)
        {
            var allowance = await _allowanceRepository.GetAllowanceByIdAsync(allowanceId);
            if (allowance == null)
            {
                return NotFound();
            }

            var allowanceDto = _mapper.Map<AllowanceDto>(allowance);
            return Ok(allowanceDto);
        }

        [HttpGet("active/{active}")]
        public async Task<IActionResult> GetAllowanceByActive(bool active)
        {
            var allowances = await _allowanceRepository.GetAllowancesByActiveAsync(active);
            var allowanceDtos = _mapper.Map<List<AllowanceDto>>(allowances);
            return Ok(allowanceDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAllowance([FromBody] AllowanceDto allowanceDto)
        {
            if (allowanceDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var allowance = _mapper.Map<Allowance>(allowanceDto);

            var createdAllowance = await _allowanceRepository.CreateAllowanceAsync(allowance);

            if (createdAllowance == null)
            {
                return StatusCode(500, "Can't create");
            }

            var createdAllowanceDto = _mapper.Map<AllowanceDto>(createdAllowance);

            return CreatedAtAction(
                actionName: nameof(GetAllowance),
                routeValues: new { allowanceId = createdAllowance.Id },
                value: createdAllowanceDto
            );
        }

        [HttpPut("{allowanceId}")]
        public async Task<IActionResult> UpdateAllowance(int allowanceId, [FromBody] AllowanceDto allowanceDto)
        {
            if (allowanceDto == null)
            {
                return BadRequest(ModelState);
            }

            if (allowanceId != allowanceDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var allowance = _mapper.Map<Allowance>(allowanceDto);

            var updatedAllowance = await _allowanceRepository.UpdateAllowanceAsync(allowance);

            if (updatedAllowance == null)
            {
                return StatusCode(500, "Can't update");
            }

            return Ok("Update successfully");
        }

        [HttpDelete("{allowanceId}")]
        public async Task<IActionResult> DeleteAllowance(int allowanceId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deletedAllowance = await _allowanceRepository.DeleteAllowanceAsync(allowanceId);

            if (deletedAllowance == null)
            {
                return StatusCode(500, "Can't delete");
            }

            return Ok("Delete successfully");
        }
    }
}
