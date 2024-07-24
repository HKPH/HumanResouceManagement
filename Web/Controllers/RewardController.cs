using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardController:ControllerBase
    {
        private readonly IRewardRepository _rewardsRepository;
        private readonly IMapper _mapper;
        public RewardController(IRewardRepository rewardsRepository, IMapper mapper)
        {
            _rewardsRepository = rewardsRepository;
            _mapper = mapper;
        }

        [HttpGet("{employeeId}")]
        public IActionResult GetRewardsByEmployeeId(int employeeId)
        {
            var rewards = _mapper.Map<List<RewardDto>>(_rewardsRepository.GetRewardsByEmployeeId(employeeId));
            return Ok(rewards);
        }

        [HttpPost]
        public IActionResult CreateReward([FromBody] RewardDto rewardCreate)
        {
            if (rewardCreate == null)
            {
                return BadRequest(ModelState);
            }
            if(!_rewardsRepository.CreateReward(_mapper.Map<Reward>(rewardCreate)))
            {
                return StatusCode(500, "Can't create");
            }
            return Ok("Create successfully");
        }

        [HttpPut]
        public IActionResult UpdateReward([FromBody] RewardDto rewardUpdate)
        {
            if (rewardUpdate == null)
            {
                return BadRequest(ModelState);
            }
            if (!_rewardsRepository.CreateReward(_mapper.Map<Reward>(rewardUpdate)))
            {
                return StatusCode(500, "Can't update");
            }
            return Ok("Update successfully");
        }

        [HttpDelete("{rewardId}")]
        public IActionResult DeleteReward(int rewardId)
        {
            if (!_rewardsRepository.DeleteReward(rewardId))
                return StatusCode(500, "Can't delete");
            return Ok("Delete successfully");
        }
    }
}
