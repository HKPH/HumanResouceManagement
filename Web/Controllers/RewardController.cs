using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardController : ControllerBase
    {
        private readonly IRewardRepository _rewardsRepository;
        private readonly IMapper _mapper;

        public RewardController(IRewardRepository rewardsRepository, IMapper mapper)
        {
            _rewardsRepository = rewardsRepository;
            _mapper = mapper;
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetRewardsByEmployeeId(int employeeId)
        {
            var rewards = await _rewardsRepository.GetRewardsByEmployeeIdAsync(employeeId);
            var rewardsDto = _mapper.Map<List<RewardDto>>(rewards);
            return Ok(rewardsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReward([FromBody] RewardDto rewardDto)
        {
            if (rewardDto == null)
            {
                return BadRequest(ModelState);
            }
            var reward = _mapper.Map<Reward>(rewardDto);
            var createdReward = await _rewardsRepository.CreateRewardAsync(reward);
            var createdRewardDto = _mapper.Map<RewardDto>(createdReward);
            if (createdReward == null)
            {
                return StatusCode(500, "Can't create");
            }
            return CreatedAtAction(
                nameof(GetRewardsByEmployeeId), 
                new { employeeId = createdReward.EmployeeId} , 
                createdRewardDto);
        }

        [HttpPut("{rewardId}")]
        public async Task<IActionResult> UpdateReward(int rewardId, [FromBody] RewardDto rewardDto)
        {
            if (rewardDto == null)
            {
                return BadRequest(ModelState);
            }
            var reward = _mapper.Map<Reward>(rewardDto);
            var updatedReward = await _rewardsRepository.UpdateRewardAsync(reward);
            if (updatedReward == null)
            {
                return StatusCode(500, "Can't update");
            }
            return Ok("Update successfully");
        }

        [HttpDelete("{rewardId}")]
        public async Task<IActionResult> DeleteReward(int rewardId)
        {
            var deletedReward = await _rewardsRepository.DeleteRewardAsync(rewardId);
            if (deletedReward == null)
            {
                return StatusCode(500, "Can't delete");
            }
            return Ok("Delete successfully");
        }
    }
}
