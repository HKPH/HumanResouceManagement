using AutoMapper;
using HumanManagement.Data.Repository;
using HumanManagement.Models.Dto;

using HumanManagement.Models;

using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class BankBranchController: ControllerBase
    {
        public readonly IBankBranchRepository _bankBranchRepository;
        public readonly IBankRepository _bankRepository;
        public readonly IMapper _mapper;

        public BankBranchController(IBankBranchRepository bankBranchRepository,IMapper mapper, IBankRepository bankRepository) 
        {
            _mapper = mapper;
            _bankBranchRepository = bankBranchRepository;
            _bankRepository=bankRepository;
        }
        [HttpGet]
        public IActionResult GetBankBranches()
        {
            var bankBranches= _bankBranchRepository.GetBankBranches();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(bankBranches);
        }
        [HttpGet("{bankBranchId}")]
        public IActionResult GetBankBranch(int bankBranchId)
        {
            var bankBranch=_bankBranchRepository.GetBankBranchById(bankBranchId);
            if(!ModelState.IsValid)
                return BadRequest(ModelState); 
            return Ok(bankBranch);
        }
        [HttpPost]
        public IActionResult CreateBankBranch([FromBody] BankBranchDto bankBranchCreate, [FromQuery] int bankId )
        {
            if(bankBranchCreate==null)
            {
                return BadRequest(ModelState);
            }    
            var bank=_bankRepository.GetBankById(bankId);
            var bankBranch=_mapper.Map<BankBranch>(bankBranchCreate);
            bankBranch.Bank=bank;
            if(!_bankBranchRepository.CreateBankBranch(bankBranch))
            {
                ModelState.AddModelError("", "Can't save bank branch");
                return StatusCode(500,ModelState);
            }    
            return Ok("Bank branch create successfully");
        }    
    }
}
