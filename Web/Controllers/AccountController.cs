using AutoMapper;
using HumanManagement.Data.Repository;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAccounts()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var account = _mapper.Map<List<AccountDto>>(_accountRepository.GetAccounts());
            return Ok(account);
        }
        [HttpGet("{accountId}")]
        public IActionResult GetAccountById(int accountId) 
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var account=_mapper.Map<AccountDto>(_accountRepository.GetAccountById(accountId));
            return Ok(account);
        }

        [HttpGet("active/{active}")]
        public IActionResult GetAccountsByActive(bool active)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 
            var accounts=_mapper.Map<List<AccountDto>>(_accountRepository.GetAccountsByActive(active));
            return Ok(accounts);
        }        
            
        [HttpPost]
        public IActionResult CreateAccount([FromBody] AccountDto accountCreate)
        {
            if (accountCreate == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var account = _accountRepository.CheckAccountByUsernameAndPassword(accountCreate);
            if (account == null)
            {
                ModelState.AddModelError("", "Account already exits");
                return StatusCode(500, ModelState);
            }
            if (!_accountRepository.CreateAccount(_mapper.Map<Account>(accountCreate)))
                if (!_accountRepository.CreateAccount(account))
                {
                    ModelState.AddModelError("", "Can't create account");
                    return StatusCode(500, ModelState);
                }
            return Ok("Account create successfully");
        }
        [HttpPut("{accountId}")]
        public IActionResult UpdateAccount([FromBody] AccountDto accountUpdate,int accountId)
        {
            if(accountUpdate==null)
                return BadRequest(ModelState);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var checkAccount=_accountRepository.GetAccountById(accountId);
            if(checkAccount == null)
            {
                return NotFound();
            }    
            var account=_mapper.Map<Account>(accountUpdate);
            if(!_accountRepository.UpdateAccount(account))
            {
                ModelState.AddModelError("", "Can't update account");
                return StatusCode(500,ModelState);
            }
            return Ok("Account has been updated successfully");
        }

        [HttpDelete("{accountId}")]
        public IActionResult DeleteAccount(int accountId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var accountDelete=_accountRepository.GetAccountById(accountId);
            if(!_accountRepository.DeleteAccount(accountDelete))
            {
                ModelState.AddModelError("", "Can't delete account");
                return StatusCode(500,ModelState);
            }
            return Ok("Account has been deleted successfully");
        }

    }
}
