﻿using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenefitController: ControllerBase
    {
        private readonly IBenefitRepository _benefitRepository;
        private readonly IMapper _mapper;
        public BenefitController(IBenefitRepository benefitRepository, IMapper mapper)
        {
            _benefitRepository = benefitRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetBenefits()
        {
            var benefits=_benefitRepository.GetBenefits();
            return Ok(benefits);
        }
        [HttpGet("{benefitId}")]
        public IActionResult GetBenefit(int benefitId)
        {
            var benefit=_benefitRepository.GetBenefitById(benefitId);
            return Ok(benefit);
        }
        [HttpGet("active/{active}")]
        public IActionResult GetBenefitByActive(bool active)
        {
            var benefits = _benefitRepository.GetBenefitsByActive(active);
            return Ok(benefits);
        }
        [HttpPost]
        public IActionResult CreateBenefit([FromBody] Benefit benefit)
        {
            if (benefit == null)
                return BadRequest(ModelState);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!_benefitRepository.CreateBenefit(benefit))
            {
                ModelState.AddModelError("", "Can't create benefit");
                return StatusCode(500, ModelState);
            }
            return Ok("Create benefit successfully");
        }
        [HttpPut]
        public IActionResult UpdateBenefit([FromBody] Benefit benefit)
        {
            if (benefit == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_benefitRepository.UpdateBenefit(benefit))
            {
                ModelState.AddModelError("", "Can't update benefit");
                return StatusCode(500, ModelState);
            }
            return Ok("Update benefit successfully");
        }
        [HttpDelete("{benefitId}")]
        public IActionResult DeleteBenefit(int benefitId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_benefitRepository.DeleteBenefit(benefitId))
            {
                ModelState.AddModelError("", "Can't delete benefit");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete benefit successfully");
        }
    }
}