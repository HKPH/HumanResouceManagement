using AutoMapper;
using HumanManagement.Data.Repository;
using HumanManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractTypeController: ControllerBase
    {
        private readonly IContractTypeRepository _contractTypeRepository;
        private readonly IMapper _mapper;
        public ContractTypeController(IContractTypeRepository contractTypeRepository, IMapper mapper)
        {
            _contractTypeRepository = contractTypeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetContractTypes()
        {
            var contractType=_contractTypeRepository.GetContractTypes();
            return Ok(contractType);
        }
        [HttpGet("{contractTypeId}")]
        public IActionResult GetContractType(int contractTypeId) 
        {
            var contractType=_contractTypeRepository.GetContractTypeById(contractTypeId);
            if (contractType == null)
            {
                return NotFound();
            }
            return Ok(contractType);
        
        }
        [HttpGet("active/{active}")]
        public IActionResult GetContractTypeByActive(bool active)
        {
            var contractTypes = _contractTypeRepository.GetContractTypeByActive(active);
            return Ok(contractTypes);
        }
        [HttpPost]
        public IActionResult CreateContractType([FromBody] ContractType contractType)
        {
            if(contractType == null)
                return BadRequest(ModelState);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!_contractTypeRepository.CreateContractType(contractType))
            {
                ModelState.AddModelError("", "Can't create contract type");
                return StatusCode(500, ModelState);
            }
            return Ok("Create contract type successfully");
        }
        [HttpPut]
        public IActionResult UpdateContractType([FromBody] ContractType contractType)
        {
            if (contractType == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_contractTypeRepository.UpdateContractType(contractType))
            {
                ModelState.AddModelError("", "Can't update contract type");
                return StatusCode(500, ModelState);
            }
            return Ok("Update contract type successfully");
        }
        [HttpDelete]
        public IActionResult DeleteContractType([FromBody] ContractType contractType)
        {
            if (contractType == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_contractTypeRepository.DeleteContractType(contractType))
            {
                ModelState.AddModelError("", "Can't delete contract type");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete contract type successfully");
        }



    }
}
