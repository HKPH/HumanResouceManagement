using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplineController : ControllerBase
    {
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IMapper _mapper;

        public DisciplineController(IDisciplineRepository disciplineRepository, IMapper mapper)
        {
            _disciplineRepository = disciplineRepository;
            _mapper = mapper;
        }

        [HttpGet("{disciplineId}")]
        public async Task<IActionResult> GetDisciplineById(int disciplineId)
        {
            var discipline = await _disciplineRepository.GetDisciplineByIdAsync(disciplineId);
            var disciplineDto = _mapper.Map<List<DisciplineDto>>(discipline);
            return Ok(disciplineDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetDisciplinesByEmployeeId([FromQuery] int employeeId)
        {
            var disciplines = await _disciplineRepository.GetDisciplinesByEmployeeIdAsync(employeeId);
            var disciplineDtos = _mapper.Map<List<DisciplineDto>>(disciplines);
            return Ok(disciplineDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscipline([FromBody] DisciplineDto disciplineDto)
        {
            if (disciplineDto == null)
                return BadRequest(ModelState);

            var discipline = _mapper.Map<Discipline>(disciplineDto);
            var createdDiscipline = await _disciplineRepository.CreateDisciplineAsync(discipline);

            if (createdDiscipline == null)
            {
                return StatusCode(500, "Can't create");
            }
            var createdDisciplineDto = _mapper.Map<DisciplineDto>(createdDiscipline);
            return CreatedAtAction(
                nameof(GetDisciplineById),
                new { disciplineId = createdDiscipline.Id },
                createdDisciplineDto);
        }

        [HttpPut("{disciplineUpdateId}")]
        public async Task<IActionResult> UpdateDiscipline([FromBody] DisciplineDto disciplineDto)
        {
            if (disciplineDto == null)
                return BadRequest(ModelState);

            var discipline = _mapper.Map<Discipline>(disciplineDto);
            var updatedDiscipline = await _disciplineRepository.UpdateDisciplineAsync(discipline);

            if (updatedDiscipline == null)
            {
                return StatusCode(500, "Can't update");
            }

            return Ok("Update successfully");
        }

        [HttpDelete("{disciplineId}")]
        public async Task<IActionResult> DeleteDiscipline(int disciplineId)
        {
            var deletedDiscipline = await _disciplineRepository.DeleteDisciplineAsync(disciplineId);

            if (deletedDiscipline == null)
            {
                return StatusCode(500, "Can't delete");
            }

            return Ok("Delete successfully");
        }
    }
}
