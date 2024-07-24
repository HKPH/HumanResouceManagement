using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[conmtroller]")]
    [ApiController]
    public class DisciplineController: ControllerBase
    {
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IMapper _mapper;
        public DisciplineController(IDisciplineRepository disciplineRepository, IMapper mapper)
        {
            _disciplineRepository = disciplineRepository;
            _mapper = mapper;
        }
        [HttpGet("{employeeId}")]
        public IActionResult GetDisciplinesByEmployeeId(int employeeId)
        {
            var disciplines = _mapper.Map<List<DisciplineDto>>(_disciplineRepository.GetDisciplinesByEmployeeId(employeeId));
            return Ok(disciplines);
        }
        [HttpPost]
        public IActionResult CreateDiscipline([FromBody] DisciplineDto disciplineCreate)
        {
            if (disciplineCreate == null)
                return BadRequest(ModelState);
            if (_disciplineRepository.CreateDiscipline(_mapper.Map<Discipline>(disciplineCreate)))
            {
                return StatusCode(500, "Can't create");
            }
            return Ok("Create successfully");

        }

        [HttpPut]
        public IActionResult UpdateDiscipline([FromBody] DisciplineDto disciplineUpdate)
        {
            if (disciplineUpdate == null)
                return BadRequest(ModelState);
            if (_disciplineRepository.UpdateDiscipline(_mapper.Map<Discipline>(disciplineUpdate)))
            {
                return StatusCode(500, "Can't update");
            }
            return Ok("Update successfully");
        }

        [HttpDelete("{disciplineId}")]
        public IActionResult DeleDiscipline(int disciplineId)
        {
\
            if (_disciplineRepository.DeleteDiscipline(disciplineId))
            {
                return StatusCode(500, "Can't delete");
            }
            return Ok("Delete successfully");
        }
    }
}
