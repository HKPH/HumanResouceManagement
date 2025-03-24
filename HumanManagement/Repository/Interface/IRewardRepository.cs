using HumanManagement.Models;

namespace HumanManagement.Repository.Interface
{
    public interface IRewardRepository
    {
        Task<List<Reward>> GetRewardsAsync();
        Task<List<Reward>> GetRewardsByEmployeeIdAsync(int employeeId);
        Task<List<Reward>> GetRewardsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<Reward> GetRewardByIdAsync(int rewardId);
        Task<Reward> CreateRewardAsync(Reward reward);
        Task<Reward> UpdateRewardAsync(Reward reward);
        Task<Reward> DeleteRewardAsync(int rewardId);
    }
}
