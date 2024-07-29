using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IRewardRepository
    {
        Task<List<Reward>> GetRewardsAsync();
        Task<List<Reward>> GetRewardsByEmployeeIdAsync(int employeeId);
        Task<Reward> GetRewardByIdAsync(int rewardId);
        Task<Reward> CreateRewardAsync(Reward reward);
        Task<Reward> UpdateRewardAsync(Reward reward);
        Task<Reward> DeleteRewardAsync(int rewardId);
    }
}
