using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResignationController:ControllerBase
    {
        private readonly IResignationRepository _resignationRepository;
        private readonly IMapper _mapper;
        public ResignationController(IResignationRepository resignationRepository, IMapper mapper)
        {
            _resignationRepository = resignationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetResignations()
        {
            var resignations=_mapper.Map<List<ResignationDto>>(_resignationRepository.GetResignations());
            return Ok(resignations);
        }

        [HttpGet("accepted/{accepted}")]
        public IActionResult GetResignationsByAccepted(bool accepted)
        {
            var resignations = _mapper.Map<List<ResignationDto>>(_resignationRepository.GetResignationsByAccepted(accepted));
            return Ok(resignations);
        }

        [HttpPost]
        public IActionResult CreateResignation([FromBody]ResignationDto resignationCreate)
        {
            if (resignationCreate == null)
                return BadRequest(ModelState);
            if (_resignationRepository.CreateResignation(_mapper.Map<Resignation>(resignationCreate)))
                return StatusCode(500, "Can't create");
            return Ok("Create successfully");
        }

        [HttpPut]
        public IActionResult UpdateResignation([FromBody] ResignationDto resignationUpdate)
        {
            if (resignationUpdate == null)
                return BadRequest(ModelState);
            if (_resignationRepository.UpdateResignation(_mapper.Map<Resignation>(resignationUpdate)))
                return StatusCode(500, "Can't update");
            return Ok("Update successfully");
        }

        [HttpDelete("{resignationId}")]
        public IActionResult DeleteResignation(int resignationId)
        {
            if (_resignationRepository.DeleteResignation(resignationId))
                return StatusCode(500, "Can't delete");
            return Ok("Delete successfully");
        }

    }
}
