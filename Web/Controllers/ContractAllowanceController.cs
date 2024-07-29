using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractAllowanceController : ControllerBase
    {
        private readonly IContractAllowanceRepository _contractAllowanceRepository;
        private readonly IMapper _mapper;

        public ContractAllowanceController(IContractAllowanceRepository contractAllowanceRepository, IMapper mapper)
        {
            _contractAllowanceRepository = contractAllowanceRepository;
            _mapper = mapper;
        }

        [HttpGet("{contractId}")]
        public async Task<IActionResult> GetAllowancesByContract(int contractId)
        {
            var allowances = await _contractAllowanceRepository.GetAllowancesByContractAsync(contractId);
            var allowanceDtos = _mapper.Map<List<AllowanceDto>>(allowances);
            return Ok(allowanceDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContractAllowance([FromBody] ContractAllowanceDto caCreate)
        {
            if (caCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contractAllowance = _mapper.Map<ContractAllowance>(caCreate);
            var createdContractAllowance = await _contractAllowanceRepository.CreateContractAllowanceAsync(contractAllowance);

            if (createdContractAllowance == null)
            {
                ModelState.AddModelError("", "Can't create");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201, "Create successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContractAllowance([FromBody] ContractAllowanceDto caUpdate)
        {
            if (caUpdate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contractAllowance = _mapper.Map<ContractAllowance>(caUpdate);
            var updatedContractAllowance = await _contractAllowanceRepository.UpdateContractAllowanceAsync(contractAllowance);

            if (updatedContractAllowance == null)
            {
                ModelState.AddModelError("", "Can't update");
                return StatusCode(500, ModelState);
            }

            return Ok("Update successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContractAllowance([FromBody] ContractAllowanceDto caDelete)
        {
            if (caDelete == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deletedContractAllowance = await _contractAllowanceRepository.DeleteContractAllowanceAsync(caDelete.ContractTypeId, caDelete.AllowanceId);

            if (deletedContractAllowance == null)
            {
                ModelState.AddModelError("", "Can't delete");
                return StatusCode(500, ModelState);
            }

            return Ok("Delete successfully");
        }
    }
}
