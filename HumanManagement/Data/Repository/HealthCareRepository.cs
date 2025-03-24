using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class HealthCareRepository : IHealthCareRepository
    {
        private readonly DBContext _context;

        public HealthCareRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<HealthCare> CreateHealthCareAsync(HealthCare healthCare)
        {
            await _context.HealthCares.AddAsync(healthCare);
            await _context.SaveChangesAsync();;
            return healthCare;
        }

        public async Task<HealthCare> DeleteHealthCareAsync(int healthCareId)
        {
            var healthCare = await GetHealthCareByIdAsync(healthCareId);
            if (healthCare == null)
            {
                return null;
            }

            _context.Remove(healthCare);
            await _context.SaveChangesAsync();;
            return healthCare;
        }

        public async Task<List<HealthCare>> GetHealthCaresByActiveAsync(bool active)
        {
            return await _context.HealthCares.Where(h => h.Active == active).ToListAsync();
        }

        public async Task<HealthCare> GetHealthCareByIdAsync(int healthCareId)
        {
            return await _context.HealthCares.FirstOrDefaultAsync(h => h.Id == healthCareId);
        }

        public async Task<List<HealthCare>> GetHealthCaresAsync()
        {
            return await _context.HealthCares.OrderBy(h => h.Id).ToListAsync();
        }

        public async Task<HealthCare> UpdateHealthCareAsync(HealthCare healthCare)
        {
            var healthCareUpdate = await GetHealthCareByIdAsync(healthCare.Id);
            if (healthCareUpdate == null)
                return null;

            _context.Entry(healthCareUpdate).CurrentValues.SetValues(healthCare);
            await _context.SaveChangesAsync();;
            return healthCareUpdate;
        }

    }
}
