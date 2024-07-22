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
        public IActionResult GetBenefitsByContract(int contractId)
        {
            var benefits = _contractBenefitRepository.GetBenefitsByContract(contractId);
            return Ok(benefits);
        }
        [HttpPost]
        public IActionResult CreateContractBenefit([FromBody] ContractBenefitDto cbCreate)
        {
            if (cbCreate == null)
                return BadRequest(ModelState);
            if (!_contractBenefitRepository.CreateContractBenefit(_mapper.Map<ContractBenefit>(cbCreate)))
            {
                ModelState.AddModelError("", "Can't create");
                return StatusCode(500, ModelState);
            }
            return Ok("Create successfully");
        }
        [HttpPut]
        public IActionResult UpdateContractBenefit([FromBody] ContractBenefitDto cbUpdate)
        {
            if (cbUpdate == null)
                return BadRequest(ModelState);
            if (!_contractBenefitRepository.UpdateContractBenefit(_mapper.Map<ContractBenefit>(cbUpdate)))
            {
                ModelState.AddModelError("", "Can't update");
                return StatusCode(500, ModelState);
            }
            return Ok("Update successfully");
        }
        [HttpDelete]
        public IActionResult DeleteContractBenefit([FromBody] ContractBenefitDto cbDelete)
        {
            if (cbDelete == null)
                return BadRequest(ModelState);
            if (!_contractBenefitRepository.DeleteContractBenefit(cbDelete.ContractTypeId, cbDelete.BenefitId))
            {
                ModelState.AddModelError("", "Can't delete");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete successfully");
        }


    }
}
