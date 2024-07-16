using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
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
        public IActionResult GetAllowances()
        {
            var allowances = _allowanceRepository.GetAllowances();
            return Ok(allowances);
        }
        [HttpGet("{allowanceId}")]
        public IActionResult GetAllowance(int allowanceId)
        {
            var allowance=_allowanceRepository.GetAllowanceById(allowanceId);
            if (allowance == null)
            {
                return NotFound();
            }
            return Ok(allowance);
        }
        [HttpGet("active/{active}")]
        public IActionResult GetAllowanceByActive(bool active)
        {
            var allowances=_allowanceRepository.GetAllowancesByActive(active);
            return Ok(allowances);
        }
        [HttpPost]
        public IActionResult CreateAllowance(Allowance allowance)
        {
            if (allowance == null)
                return BadRequest(ModelState);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!_allowanceRepository.CreateAllowance(allowance))
            {
                ModelState.AddModelError("", "Can't create allowance");
            }
            return Ok("Create allowance successfully");
        }
        [HttpPut]
        public IActionResult UpdateAllowance(Allowance allowance)
        {
            if (allowance == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_allowanceRepository.UpdateAllowance(allowance))
            {
                ModelState.AddModelError("", "Can't update allowance");
            }
            return Ok("Update allowance successfully");
        }
        [HttpDelete("{allwanceId}")]
        public IActionResult DeleteAllowance(int allowanceId)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_allowanceRepository.DeleteAllowance(allowanceId))
            {
                ModelState.AddModelError("", "Can't delete allowance");
            }
            return Ok("Delete allowance successfully");
        }

    }
}
