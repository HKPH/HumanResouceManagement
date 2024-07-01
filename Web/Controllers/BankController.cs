using AutoMapper;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using HumanManagement.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{

    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class BankController:ControllerBase
    {
        private readonly IBankRepository _bankRepository;
        private readonly IMapper _mapper;
        public BankController(IBankRepository bankRepository, IMapper mapper) 
        {
            _bankRepository = bankRepository;
            _mapper=mapper;
        }
        [HttpGet]
        public IActionResult GetBanks()
        {
            var banks=_mapper.Map<List<BankDto>>(_bankRepository.GetBanks());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(banks);
        }
        [HttpGet("{bankId}")]
        public IActionResult GetBank(int bankId )
        {
            if(!_bankRepository.HasBank(bankId))
            {
                return NotFound();
            }    
            var bank=_mapper.Map<BankDto>(_bankRepository.GetBankById(bankId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(bank);
        }
        

    }
}
