using AutoMapper;
using HumanManagement.Data.Repository;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult GetBanks()
        {
            var banks = _mapper.Map<List<BankDto>>(_bankRepository.GetBanks());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(banks);
        }

        [HttpGet("{bankId}")]
        public IActionResult GetBank(int bankId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bankRepository.HasBank(bankId))
            {
                return NotFound();
            }

            var bank = _mapper.Map<BankDto>(_bankRepository.GetBankById(bankId));
            return Ok(bank);
        }

        [HttpGet("active/{active}")]
        public IActionResult GetBankByActive(bool active)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var banks=_mapper.Map<List<BankDto>>(_bankRepository.GetBanksByActive(active));
            return Ok(banks);
        }

        [HttpPost]
        public IActionResult CreateBank([FromBody] BankDto bankCreate)
        {
            if (bankCreate == null)
            {
                return BadRequest(ModelState);
            }

            var bank = _bankRepository.checkBankByName(_mapper.Map<Bank>(bankCreate));

            if (bank != null)
            {
                ModelState.AddModelError("", "Bank already exits");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bankCreate.Active = true;

            var bankMap = _mapper.Map<Bank>(bankCreate);

            if (!_bankRepository.CreateBank(bankMap))
            {
                ModelState.AddModelError("", "Bank cant crate");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{bankId}")]
        public IActionResult UpdateBank(int bankId, [FromBody] BankDto bankUpdate)
        {
            if (bankUpdate == null)
            {
                return BadRequest(ModelState);
            }
            if(!_bankRepository.HasBank(bankId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bankMap=_mapper.Map<Bank>(bankUpdate);
            if(!_bankRepository.UpdateBank(bankMap))
            {
                ModelState.AddModelError("", "Can't update bank");
                return StatusCode(500,ModelState);
            }
            return Ok("Update Successfully");
        }

        [HttpDelete("{bankId}")]
        public IActionResult DeleteBank([FromRoute] int bankId)
        {
            try
            {
                if (!_bankRepository.HasBank(bankId))
                {
                    return NotFound();
                }

                var bankToDelete = _bankRepository.GetBankById(bankId);
                if (bankToDelete == null)
                {
                    return NotFound();
                }

                if (!_bankRepository.DeleteBank(bankId))
                {
                    ModelState.AddModelError("", "Can't delete bank");
                    return StatusCode(500, ModelState);
                }

                return Ok("Delete bank successfully");
            }
            catch (DbUpdateException ex)
            {

                ModelState.AddModelError("", "Cannot delete this bank due to database conflict.");
                return StatusCode(409, ModelState); 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }
        [HttpPut("active/{bankId}")]
        public IActionResult ChangeBankActive(int bankId,[FromBody] bool active)
        {
            var bank=_bankRepository.GetBankById(bankId);
            if(bank== null)
            {
                return NotFound();
            }    
            bank.Active = active;
            if (!_bankRepository.UpdateBank(bank))
            {
                ModelState.AddModelError("", $"Can't change bank status to {active}");
                return StatusCode(500, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok($"Bank status changes to {active}");
        }



    }
}
