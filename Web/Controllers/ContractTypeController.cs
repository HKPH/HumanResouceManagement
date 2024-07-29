using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;


namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractTypeController : ControllerBase
    {
        private readonly IContractTypeRepository _contractTypeRepository;
        private readonly IMapper _mapper;

        public ContractTypeController(IContractTypeRepository contractTypeRepository, IMapper mapper)
        {
            _contractTypeRepository = contractTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetContractTypes()
        {
            var contractTypes = _mapper.Map<List<ContractTypeDto>>(await _contractTypeRepository.GetContractTypesAsync());
            return Ok(contractTypes);
        }

        [HttpGet("{contractTypeId}")]
        public async Task<IActionResult> GetContractType(int contractTypeId)
        {
            var contractType = _mapper.Map<ContractTypeDto>(await _contractTypeRepository.GetContractTypeByIdAsync(contractTypeId));
            if (contractType == null)
            {
                return NotFound();
            }
            return Ok(contractType);
        }

        [HttpGet("active/{active}")]
        public async Task<IActionResult> GetContractTypesByActive(bool active)
        {
            var contractTypes = _mapper.Map<List<ContractTypeDto>>(await _contractTypeRepository.GetContractTypesByActiveAsync(active));
            return Ok(contractTypes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContractType([FromBody] ContractTypeDto contractTypeDto)
        {
            if (contractTypeDto == null)
                return BadRequest("Invalid data.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contractType = _mapper.Map<ContractType>(contractTypeDto);
            var createdContractType = await _contractTypeRepository.CreateContractTypeAsync(contractType);

            if (createdContractType == null)
            {
                ModelState.AddModelError("", "Can't create contract type");
                return StatusCode(500, ModelState);
            }
            var createdContractTypeDto = _mapper.Map<ContractTypeDto>(createdContractType);
            return CreatedAtAction(
                nameof(GetContractType),
                new { contractTypeId = createdContractType.Id },
                createdContractTypeDto);
        }

        [HttpPut("{contractTypeId}")]
        public async Task<IActionResult> UpdateContractType(int contractTypeId, [FromBody] ContractTypeDto contractTypeDto)
        {
            if (contractTypeDto == null)
                return BadRequest("Invalid data.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(contractTypeId != contractTypeDto.Id)
                return BadRequest(ModelState);

            var contractType = _mapper.Map<ContractType>(contractTypeDto);
            var result = await _contractTypeRepository.UpdateContractTypeAsync(contractType);

            if (result == null)
            {
                ModelState.AddModelError("", "Can't update contract type");
                return StatusCode(500, ModelState);
            }

            return Ok("Update contract type successfully");
        }

        [HttpDelete("{contractTypeId}")]
        public async Task<IActionResult> DeleteContractType(int contractTypeId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _contractTypeRepository.DeleteContractTypeAsync(contractTypeId);

            if (result == null)
            {
                ModelState.AddModelError("", "Can't delete contract type");
                return StatusCode(500, ModelState);
            }

            return Ok("Delete contract type successfully");
        }
    }
}
