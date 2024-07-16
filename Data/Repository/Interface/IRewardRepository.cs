using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IRewardRepository
    {
        List<Reward> GetRewards();
        Reward GetRewardById(int rewardId);
        bool Save();
        bool CreateReward(Reward reward);
        bool UpdateReward(Reward reward);
        bool DeleteReward(int rewardId);
    }
}
