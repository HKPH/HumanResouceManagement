﻿using AutoMapper;
using HumanManagement.Data.Repository;
using HumanManagement.Models.Dto;
using HumanManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult GetHealthCares()
        {
            var healthCares=_mapper.Map<List<HealthCareDto>>(_healthCareRepository.GetHealthCares());
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(healthCares);

        }
        [HttpGet("{healthCareId}")]
        public IActionResult GetHealthCare(int healthCareId)
        {
            var healthCare=_mapper.Map<HealthCare>(_healthCareRepository.GetHealthCareById(healthCareId));
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(healthCare);
        }
        [HttpGet("active/{active}")]
        public IActionResult GetHealthCareByActive(bool active)
        {
            var healthCares = _mapper.Map<List<HealthCareDto>>(_healthCareRepository.GetHealthCareByActive(active));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(healthCares);
        }
        [HttpPost]
        public IActionResult PostHealthCare([FromBody] HealthCareDto healthCareCreate)
        {
            if (healthCareCreate == null)
            {
                return BadRequest(ModelState);
            }
            healthCareCreate.Active= true;
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_healthCareRepository.CreateHealthCare(_mapper.Map<HealthCare>(healthCareCreate)))
            {
                ModelState.AddModelError("", "Can't create health care ");
                return StatusCode(500, ModelState);
            }
            return Ok("Create health care successfully");
        }
        [HttpPut]
        public IActionResult UpdateHealthCare([FromBody]HealthCare healthCareUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if(healthCareUpdate==null)
            {
                return BadRequest(ModelState);
            }
            if (!_healthCareRepository.UpdateHealthCare(healthCareUpdate))
            {
                ModelState.AddModelError("", "Can't update health care");
            }
            return Ok("Update health care successfully");
        }
        [HttpDelete]
        public IActionResult DeteteHealthCare([FromBody] HealthCare healthCareDelete)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (healthCareDelete == null)
            {
                return BadRequest(ModelState);
            }
            if (!_healthCareRepository.DeleteHealthCare(healthCareDelete))
            {
                ModelState.AddModelError("", "Can't delete health care");
            }
            return Ok("Delete health care successfully");
        }

    }
}
