using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public class RewardRepository:IRewardRepository
    {
        private readonly DBContext _context;
        public RewardRepository(DBContext context)
        {
            _context = context;
        }

        public bool CreateReward(Reward reward)
        {
            _context.Add(reward);
            return Save();
        }

        public bool DeleteReward(int rewardId)
        {
            throw new NotImplementedException();
        }

        public Reward GetRewardById(int rewardId)
        {
            return _context.Rewards.FirstOrDefault(r => r.Id == rewardId);
        }

        public List<Reward> GetRewards()
        {
            return _context.Rewards.OrderBy(r => r.Id).ToList();
        }


        public List<Reward> GetRewardsByEmployeeId(int employeeId)
        {
            return _context.Rewards.Where(e=>e.EmployeeId == employeeId).ToList();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateReward(Reward reward)
        {
            var rewardUpdate=GetRewardById(reward.Id);
            _context.Entry(rewardUpdate).CurrentValues.SetValues(reward);
            return Save();
        }
    }
}
