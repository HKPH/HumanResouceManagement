using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace HumanManagement.Data.Repository
{
    public class BenefitRepository : IBenefitRepository
    {
        private readonly DBContext _context;

        public BenefitRepository(DBContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Benefit> CreateBenefitAsync(Benefit benefit)
        {
            await _context.Benefits.AddAsync(benefit);
            await SaveAsync();
            return benefit;
        }

        public async Task<Benefit> DeleteBenefitAsync(int benefitId)
        {
            var benefitDelete = await GetBenefitByIdAsync(benefitId);
            if (benefitDelete == null)
                return null;
            _context.Benefits.Remove(benefitDelete);
            await SaveAsync();
            return benefitDelete;
        }

        public async Task<List<Benefit>> GetBenefitsByActiveAsync(bool active)
        {
            return await _context.Benefits.Where(b => b.Active == active).ToListAsync();
        }

        public async Task<Benefit> GetBenefitByIdAsync(int benefitId)
        {
            return await _context.Benefits.Where(b => b.Id == benefitId).FirstOrDefaultAsync();
        }

        public async Task<List<Benefit>> GetBenefitsAsync()
        {
            return await _context.Benefits.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var check = await _context.SaveChangesAsync();
            return check > 0;
        }

        public async Task<Benefit> UpdateBenefitAsync(Benefit benefit)
        {
            var benefitUpdate = await GetBenefitByIdAsync(benefit.Id);
            if (benefitUpdate == null)
                return null;
            _context.Entry(benefitUpdate).CurrentValues.SetValues(benefit);
            await SaveAsync();
            return benefit;
        }
    }
}
