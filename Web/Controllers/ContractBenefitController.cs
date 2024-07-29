using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;


namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractBenefitController : ControllerBase
    {
        private readonly IContractBenefitRepository _contractBenefitRepository;
        private readonly IMapper _mapper;

        public ContractBenefitController(IContractBenefitRepository contractBenefitRepository, IMapper mapper)
        {
            _contractBenefitRepository = contractBenefitRepository;
            _mapper = mapper;
        }

        [HttpGet("{contractId}")]
        public async Task<IActionResult> GetBenefitsByContract(int contractId)
        {
            var benefits = await _contractBenefitRepository.GetBenefitsByContractAsync(contractId);
            return Ok(benefits);
        }
        [HttpGet]
        public async Task<IActionResult> GetBenefitContract([FromQuery]int contractId, [FromQuery] int benefitId)
        {
            var benefit = await _contractBenefitRepository.GetContractBenefitAsync(contractId, benefitId);
            return Ok(benefit);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContractBenefit([FromBody] ContractBenefitDto cbCreate)
        {
            if (cbCreate == null)
                return BadRequest(ModelState);

            var contractBenefit = _mapper.Map<ContractBenefit>(cbCreate);
            var createdContractBenefit = await _contractBenefitRepository.CreateContractBenefitAsync(contractBenefit);

            if (createdContractBenefit == null)
            {
                ModelState.AddModelError("", "Can't create");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201, "Create successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContractBenefit([FromBody] ContractBenefitDto cbUpdate)
        {
            if (cbUpdate == null)
                return BadRequest("Invalid data.");

            var contractBenefit = _mapper.Map<ContractBenefit>(cbUpdate);
            var result = await _contractBenefitRepository.UpdateContractBenefitAsync(contractBenefit);

            if (result == null)
            {
                ModelState.AddModelError("", "Can't update");
                return StatusCode(500, ModelState);
            }

            return Ok("Update successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContractBenefit([FromBody] ContractBenefitDto cbDelete)
        {
            if (cbDelete == null)
                return BadRequest("Invalid data.");

            var result = await _contractBenefitRepository.DeleteContractBenefitAsync(cbDelete.ContractTypeId, cbDelete.BenefitId);

            if (result == null)
            {
                ModelState.AddModelError("", "Can't delete");
                return StatusCode(500, ModelState);
            }

            return Ok("Delete successfully");
        }
    }
}
