using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeCvController : ControllerBase
    {
        private readonly IEmployeeCvRepository _employeeCvRepository;
        private readonly IMapper _mapper;

        public EmployeeCvController(IEmployeeCvRepository employeeCvRepository, IMapper mapper)
        {
            _employeeCvRepository = employeeCvRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeCvs()
        {
            var employeeCvs = await _employeeCvRepository.GetEmployeeCvsAsync();
            var employeeCvsDto = _mapper.Map<List<EmployeeCvDto>>(employeeCvs);
            return Ok(employeeCvsDto);
        }

        [HttpGet("{employeeCvId}")]
        public async Task<IActionResult> GetEmployeeCv(int employeeCvId)
        {
            var employeeCv = await _employeeCvRepository.GetEmployeeCvByIdAsync(employeeCvId);
            if (employeeCv == null)
            {
                return NotFound("Employee CV not found.");
            }
            var employeeCvDto = _mapper.Map<EmployeeCvDto>(employeeCv);
            return Ok(employeeCvDto);
        }

        [HttpGet("active/{active}")]
        public async Task<IActionResult> GetEmployeeCvsByActive(bool active)
        {
            var employeeCvs = await _employeeCvRepository.GetEmployeeCvsByActiveAsync(active);
            var employeeCvsDto = _mapper.Map<List<EmployeeCvDto>>(employeeCvs);
            return Ok(employeeCvsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeCv([FromBody] EmployeeCvDto employeeCvDto)
        {
            if (employeeCvDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeCv = _mapper.Map<EmployeeCv>(employeeCvDto);
            var createdEmployeeCv = await _employeeCvRepository.CreateEmployeeCvAsync(employeeCv);

            if (createdEmployeeCv == null)
            {
                return StatusCode(500, "Can't create");
            }

            var createdEmployeeCvDto = _mapper.Map<EmployeeCvDto>(createdEmployeeCv);
            return CreatedAtAction(
                nameof(GetEmployeeCv),
                new { employeeCvId = createdEmployeeCv.Id },
                createdEmployeeCvDto);
        }

        [HttpPut("{employeeCvId}")]
        public async Task<IActionResult> UpdateEmployeeCv(int employeeCvId, [FromBody] EmployeeCvDto employeeCvDto)
        {
            if (employeeCvDto == null)
            {
                return BadRequest("Employee CV data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(employeeCvId != employeeCvDto.Id)
                return BadRequest(ModelState);

            var employeeCv = _mapper.Map<EmployeeCv>(employeeCvDto);
            var updatedEmployeeCv = await _employeeCvRepository.UpdateEmployeeCvAsync(employeeCv);

            if (updatedEmployeeCv == null)
            {
                return StatusCode(500, "Failed to update employee CV.");
            }

            var updatedEmployeeCvDto = _mapper.Map<EmployeeCvDto>(updatedEmployeeCv);
            return Ok(updatedEmployeeCvDto);
        }

        [HttpDelete("{employeeCvId}")]
        public async Task<IActionResult> DeleteEmployeeCv(int employeeCvId)
        {
            var deletedEmployeeCv = await _employeeCvRepository.DeleteEmployeeCvAsync(employeeCvId);

            if (deletedEmployeeCv == null)
            {
                return StatusCode(500, "Can't delete");
            }

            var deletedEmployeeCvDto = _mapper.Map<EmployeeCvDto>(deletedEmployeeCv);
            return Ok(deletedEmployeeCvDto);
        }
    }
}
