using AutoMapper;
using HumanManagement.Data.Repository;
using HumanManagement.Models.Dto;
using HumanManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class HealthCareController: ControllerBase
    {
        private readonly IHealthCareRepository _healthCareRepository;
        private readonly IMapper _mapper;
        public HealthCareController(IHealthCareRepository healthCareRepository, IMapper mapper)
        {
            _healthCareRepository = healthCareRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetHealthCare()
        {
            var healthCares=_mapper.Map<List<HealthCareDto>>(_healthCareRepository.GetHealthCares());
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(healthCares);

        }
        [HttpGet("{healthCareId}")]
        public IActionResult GetHealthCareById(int healthCareId)
        {
            var healthCare=_mapper.Map<HealthCare>(_healthCareRepository.GetHealthCareById(healthCareId));
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(healthCare);
        }
        [HttpPost]
        public IActionResult PostHealthCare([FromBody] HealthCare healthCareCreate)
        {
            if (healthCareCreate == null)
            {
                return BadRequest(ModelState);
            }
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!_healthCareRepository.CreateHealthCare(healthCareCreate))
            {
                ModelState.AddModelError("", "Can't create health care ");
                return StatusCode(500, ModelState);
            }    
        }
    }
}
