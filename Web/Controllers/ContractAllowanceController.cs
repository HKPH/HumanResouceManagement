using AutoMapper;
using HumanManagement.Data.Repository;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractAllowanceController: ControllerBase
    {
        private readonly IContractAllowanceRepository _contractAllowanceRepository;
        private readonly IMapper _mapper;
        public ContractAllowanceController(IContractAllowanceRepository contractAllowanceRepository, IMapper mapper)
        {
            _contractAllowanceRepository = contractAllowanceRepository;
            _mapper = mapper;
        }
        [HttpGet("{contractId}")]
        public IActionResult GetAllowancesByContract(int contractId)
        {
            var allowances=_contractAllowanceRepository.GetAllowancesByContract(contractId);
            return Ok(allowances);
        }
        [HttpPost]
        public IActionResult CreateContractAllowance([FromBody] ContractAllowanceDto caCreate)
        {
            if (caCreate == null)
                return BadRequest(ModelState);
            if (!_contractAllowanceRepository.CreateContractAllowance(_mapper.Map<ContractAllowance>(caCreate)))
            {
                ModelState.AddModelError("", "Can't create");
                return StatusCode(500,ModelState);
            }
            return Ok("Create successfully");
        }
        [HttpPut]
        public IActionResult UpdateContractAllowance([FromBody] ContractAllowanceDto caUpdate)
        {
            if (caUpdate == null)
                return BadRequest(ModelState);
            if (!_contractAllowanceRepository.UpdateContractAllowance(_mapper.Map<ContractAllowance>(caUpdate)))
            {
                ModelState.AddModelError("", "Can't update");
                return StatusCode(500, ModelState);
            }
            return Ok("Update successfully");
        }
        [HttpDelete]
        public IActionResult DeleteContractAllowance([FromBody] ContractAllowanceDto caDelete)
        {
            if (caDelete == null)
                return BadRequest(ModelState);
            if (!_contractAllowanceRepository.DeleteContractAllowance(caDelete.ContractTypeId,caDelete.AllowanceId))
            {
                ModelState.AddModelError("", "Can't delete");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete successfully");
        }


    }
}
