using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class RewardRepository : IRewardRepository
    {
        private readonly DBContext _context;

        public RewardRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Reward> CreateRewardAsync(Reward reward)
        {
            await _context.AddAsync(reward);
            await _context.SaveChangesAsync();
            return reward;
        }

        public async Task<Reward> DeleteRewardAsync(int rewardId)
        {
            var reward = await GetRewardByIdAsync(rewardId);
            if (reward == null)
            {
                return null;
            }
            _context.Rewards.Remove(reward);
            await _context.SaveChangesAsync();
            return reward;
        }

        public async Task<Reward> GetRewardByIdAsync(int rewardId)
        {
            return await _context.Rewards.FirstOrDefaultAsync(r => r.Id == rewardId);
        }

        public async Task<List<Reward>> GetRewardsAsync()
        {
            return await _context.Rewards.OrderBy(r => r.Id).ToListAsync();
        }

        public async Task<List<Reward>> GetRewardsByEmployeeIdAsync(int employeeId)
        {
            return await _context.Rewards.Where(e => e.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<List<Reward>> GetRewardsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Rewards
                .Where(d => d.CreateDate >= startDate && d.CreateDate <= endDate)
                .ToListAsync();
        }



        public async Task<Reward> UpdateRewardAsync(Reward reward)
        {
            var rewardToUpdate = await GetRewardByIdAsync(reward.Id);
            if (rewardToUpdate == null)
            {
                return null;
            }
            _context.Entry(rewardToUpdate).CurrentValues.SetValues(reward);
            await _context.SaveChangesAsync();
            return rewardToUpdate;
        }

    }
}
