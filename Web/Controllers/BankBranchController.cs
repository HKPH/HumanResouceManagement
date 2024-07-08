﻿using AutoMapper;
using HumanManagement.Data.Repository;
using HumanManagement.Models.Dto;

using HumanManagement.Models;

using Microsoft.AspNetCore.Mvc;


namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
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
            var bankBranches= _mapper.Map<List<BankBranchDto>>(_bankBranchRepository.GetBankBranches());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(bankBranches);
        }
        [HttpGet("{bankBranchId}")]
        public IActionResult GetBankBranch(int bankBranchId)
        {
            var bankBranch=_mapper.Map<BankBranchDto>(_bankBranchRepository.GetBankBranchById(bankBranchId));
            if(!ModelState.IsValid)
                return BadRequest(ModelState); 
            return Ok(bankBranch);
        }
        [HttpGet("bankBranches/{bankId}")]
        public IActionResult GetBankBranchByBankId(int bankId)
        {
            var bankBranch = _mapper.Map<List<BankBranchDto>>( _bankBranchRepository.GetAllBankBranchesByBankId(bankId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(bankBranch);
        }
        [HttpGet("active/{active}")]
        public IActionResult GetBankBranchByActive(bool active)
        {
            var bankBranch = _mapper.Map<List<BankBranchDto>>(_bankBranchRepository.GetBankBranchesByActive(active));
            if (!ModelState.IsValid)
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
            bankBranch.Active = true;
            if(!_bankBranchRepository.CreateBankBranch(bankBranch))
            {
                ModelState.AddModelError("", "Can't save bank branch");
                return StatusCode(500,ModelState);
            }    
            return Ok("Bank branch create successfully");
        }
        [HttpPut("branch/{bankBranchId}")]
        public IActionResult UpdateBankBranch([FromBody] BankBranchDto bankBranchUpdate, [FromRoute] int bankBranchId)
        {
            if (bankBranchUpdate == null)
            {
                return BadRequest(ModelState);
            }
            if(!_bankBranchRepository.HasBankBranch(bankBranchId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bankBranch=_mapper.Map<BankBranch>(bankBranchUpdate);
            if(!_bankBranchRepository.UpdateBankBranch(bankBranch))
            {
                ModelState.AddModelError("", "Can't update bank bracnh");
                return StatusCode(500,ModelState);
            }
            return Ok("Bank branch update successfully");
        }
        [HttpDelete("{bankBranchId}")]
        public IActionResult DeleteBankBranch([FromRoute] int bankBranchId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bankBranch=_bankBranchRepository.GetBankBranchById(bankBranchId);
            if(!_bankBranchRepository.DeleteBankBranch(bankBranch))
            {
                ModelState.AddModelError("", "Can't delete bank branch");
                return StatusCode(500,ModelState);
            }
            return Ok("Delete bank branch successfully");
        }
        [HttpDelete("branches/{bankId}")]
        public IActionResult DeleteBankBranches([FromRoute] int bankId)
        {
            if (!_bankRepository.HasBank(bankId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bank=_bankRepository.GetBankById(bankId);
            var branches=_bankBranchRepository.GetAllBankBranchesByBankId(bankId);
            if(!_bankBranchRepository.DeleteBankBranches(branches))
            {
                ModelState.AddModelError("", "Can't delete branches");
                return StatusCode(500,ModelState);
            }
            return Ok("Delete bank branches successfully");
        }
        
    }
}