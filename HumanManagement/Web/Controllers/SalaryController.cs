using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryRepository _salaryRepository;
        private readonly IMapper _mapper;

        public SalaryController(ISalaryRepository salaryRepository, IMapper mapper)
        {
            _salaryRepository = salaryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSalaries()
        {
            var salaries = await _salaryRepository.GetSalariesAsync();
            var salaryDtos = _mapper.Map<List<SalaryDto>>(salaries);
            return Ok(salaryDtos);
        }

        [HttpGet("unused")]
        public async Task<IActionResult> GetSalariesNotUsing()
        {
            var salaries = await _salaryRepository.GetSalariesNotUsingAsync();
            var salaryDtos = _mapper.Map<List<SalaryDto>>(salaries);
            return Ok(salaryDtos);
        }

        [HttpGet("{salaryId}")]
        public async Task<IActionResult> GetSalaryById(int salaryId)
        {
            var salary = await _salaryRepository.GetSalaryByIdAsync(salaryId);
            var salaryDto = _mapper.Map<SalaryDto>(salary);
            return Ok(salaryDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSalary([FromBody] SalaryDto salaryDto)
        {
            if (salaryDto == null)
                return BadRequest(ModelState);

            var salary = _mapper.Map<Salary>(salaryDto);
            var createdSalary = await _salaryRepository.CreateSalaryAsync(salary);
            var createdSalaryDto = _mapper.Map<SalaryDto>(createdSalary);
            if (createdSalary == null)
                return StatusCode(500, "Can't create salary");

            return CreatedAtAction(nameof(GetSalaryById), new { salaryId = createdSalary.Id }, createdSalaryDto);
        }

        [HttpPut("{salaryId}")]
        public async Task<IActionResult> UpdateSalary(int salaryId, [FromBody] SalaryDto salaryDto)
        {
            if (salaryDto == null || salaryId != salaryDto.Id)
                return BadRequest(ModelState);

            var salary = _mapper.Map<Salary>(salaryDto);
            var updatedSalary = await _salaryRepository.UpdateSalaryAsync(salary);

            if (updatedSalary == null)
                return StatusCode(500, "Can't update salary");

            return Ok("Update salary successfully");
        }

        [HttpDelete("{salaryId}")]
        public async Task<IActionResult> DeleteSalary(int salaryId)
        {
            var deletedSalary = await _salaryRepository.DeleteSalaryAsync(salaryId);

            if (deletedSalary == null)
                return StatusCode(500, "Can't delete salary");

            return Ok("Delete salary successfully");
        }
    }
}
