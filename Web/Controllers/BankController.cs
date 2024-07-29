using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankRepository _bankRepository;
        private readonly IMapper _mapper;

        public BankController(IBankRepository bankRepository, IMapper mapper)
        {
            _bankRepository = bankRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBanks()
        {
            var banks = _mapper.Map<List<BankDto>>(await _bankRepository.GetBanksAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(banks);
        }

        [HttpGet("{bankId}")]
        public async Task<IActionResult> GetBank(int bankId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _bankRepository.HasBankAsync(bankId))
            {
                return NotFound();
            }

            var bank = _mapper.Map<BankDto>(await _bankRepository.GetBankByIdAsync(bankId));
            return Ok(bank);
        }

        [HttpGet("active/{active}")]
        public async Task<IActionResult> GetBankByActive(bool active)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var banks = _mapper.Map<List<BankDto>>(await _bankRepository.GetBanksByActiveAsync(active));
            return Ok(banks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBank([FromBody] BankDto bankCreate)
        {
            if (bankCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bankCreate.Active = true;

            var bankMap = _mapper.Map<Bank>(bankCreate);

            var createdBank = await _bankRepository.CreateBankAsync(bankMap);
            if (createdBank == null)
            {
                ModelState.AddModelError("", "Bank can't be created");
                return StatusCode(500, ModelState);
            }

            var createdBankDto = _mapper.Map<BankDto>(createdBank);

            return CreatedAtAction(
                actionName: nameof(GetBank),
                routeValues: new { bankBranchId = createdBank.Id },
                value: createdBankDto
            );
        }

        [HttpPut("{bankId}")]
        public async Task<IActionResult> UpdateBank(int bankId, [FromBody] BankDto bankUpdate)
        {
            if (bankUpdate == null)
            {
                return BadRequest(ModelState);
            }
            if (!await _bankRepository.HasBankAsync(bankId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bankMap = _mapper.Map<Bank>(bankUpdate);
            var updatedBank = await _bankRepository.UpdateBankAsync(bankMap);
            if (updatedBank == null)
            {
                ModelState.AddModelError("", "Can't update bank");
                return StatusCode(500, ModelState);
            }
            return Ok("Update successfully");
        }

        [HttpDelete("{bankId}")]
        public async Task<IActionResult> DeleteBank([FromRoute] int bankId)
        {

            if (!await _bankRepository.HasBankAsync(bankId))
            {
                return NotFound();
            }

            var bankToDelete = await _bankRepository.GetBankByIdAsync(bankId);
            if (bankToDelete == null)
            {
                return NotFound();
            }

            var deletedBank = await _bankRepository.DeleteBankAsync(bankId);
            if (deletedBank == null)
            {
                ModelState.AddModelError("", "Can't delete bank");
                return StatusCode(500, ModelState);
            }

            return Ok("Delete bank successfully");

        }

        [HttpPut("active/{bankId}")]
        public async Task<IActionResult> ChangeBankActive(int bankId, [FromBody] bool active)
        {
            var bank = await _bankRepository.GetBankByIdAsync(bankId);
            if (bank == null)
            {
                return NotFound();
            }
            bank.Active = active;
            var updatedBank = await _bankRepository.UpdateBankAsync(bank);
            if (updatedBank == null)
            {
                ModelState.AddModelError("", $"Can't change bank status to {active}");
                return StatusCode(500, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok($"Bank status changed to {active}");
        }
    }
}
