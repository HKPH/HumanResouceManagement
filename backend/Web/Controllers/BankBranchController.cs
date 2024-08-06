using AutoMapper;
using HumanManagement.Models.Dto;
using HumanManagement.Models;
using Microsoft.AspNetCore.Mvc;
using HumanManagement.Data.Repository.Interface;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankBranchController : ControllerBase
    {
        private readonly IBankBranchRepository _bankBranchRepository;
        private readonly IBankRepository _bankRepository;
        private readonly IMapper _mapper;

        public BankBranchController(IBankBranchRepository bankBranchRepository, IMapper mapper, IBankRepository bankRepository)
        {
            _mapper = mapper;
            _bankBranchRepository = bankBranchRepository;
            _bankRepository = bankRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBankBranches()
        {
            var bankBranches = _mapper.Map<List<BankBranchDto>>(await _bankBranchRepository.GetBankBranchesAsync());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(bankBranches);
        }

        [HttpGet("{bankBranchId}")]
        public async Task<IActionResult> GetBankBranch(int bankBranchId)
        {
            var bankBranch = _mapper.Map<BankBranchDto>(await _bankBranchRepository.GetBankBranchByIdAsync(bankBranchId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(bankBranch);
        }

        [HttpGet("bankBranches/{bankId}")]
        public async Task<IActionResult> GetBankBranchByBankId(int bankId)
        {
            var bankBranch = _mapper.Map<List<BankBranchDto>>(await _bankBranchRepository.GetAllBankBranchesByBankIdAsync(bankId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(bankBranch);
        }

        [HttpGet("active/{active}")]
        public async Task<IActionResult> GetBankBranchByActive(bool active)
        {
            var bankBranch = _mapper.Map<List<BankBranchDto>>(await _bankBranchRepository.GetBankBranchesByActiveAsync(active));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(bankBranch);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBankBranch([FromBody] BankBranchDto bankBranchDto, [FromQuery] int bankId)
        {
            if (bankBranchDto == null)
            {
                return BadRequest(ModelState);
            }
            var bank = await _bankRepository.GetBankByIdAsync(bankId);
            if (bank == null)
            {
                return NotFound("Bank not found");
            }
            var bankBranch = _mapper.Map<BankBranch>(bankBranchDto);
            bankBranch.Bank = bank;
            bankBranch.Active = true;
            var createdBankBranch = await _bankBranchRepository.CreateBankBranchAsync(bankBranch);
            if (createdBankBranch == null)
            {
                ModelState.AddModelError("", "Can't save bank branch");
                return StatusCode(500, ModelState);
            }
            var createdBankBranchDto = _mapper.Map<BankBranchDto>(createdBankBranch);
            return CreatedAtAction(
                actionName: nameof(GetBankBranch),
                routeValues: new { bankBranchId = createdBankBranch.Id },
                value: createdBankBranchDto
            );
        }

        [HttpPut("branch/{bankBranchId}")]
        public async Task<IActionResult> UpdateBankBranch(int bankBranchId, [FromBody] BankBranchDto bankBranchUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (bankBranchUpdate == null || bankBranchId != bankBranchUpdate.Id)
            {
                return BadRequest(ModelState);
            }

            var bankBranch = _mapper.Map<BankBranch>(bankBranchUpdate);
            var updatedBankBranch = await _bankBranchRepository.UpdateBankBranchAsync(bankBranch);
            if (updatedBankBranch == null)
            {
                ModelState.AddModelError("", "Can't update bank branch");
                return StatusCode(500, ModelState);
            }
            return Ok("Bank branch update successfully");
        }

        [HttpDelete("{bankBranchId}")]
        public async Task<IActionResult> DeleteBankBranch([FromRoute] int bankBranchId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var deletedBankBranch = await _bankBranchRepository.DeleteBankBranchAsync(bankBranchId);
            if (deletedBankBranch == null)
            {
                ModelState.AddModelError("", "Can't delete bank branch");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete bank branch successfully");
        }
    }
}
