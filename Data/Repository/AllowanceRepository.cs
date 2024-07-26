using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanManagement.Data.Repository
{
    public class AllowanceRepository : IAllowanceRepository
    {
        private readonly DBContext _context;

        public AllowanceRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Allowance> CreateAllowanceAsync(Allowance allowance)
        {
            await _context.Allowances.AddAsync(allowance);
            await SaveAsync();
            return allowance;
        }

        public async Task<Allowance> DeleteAllowanceAsync(int allowanceId)
        {
            var allowance = await GetAllowanceByIdAsync(allowanceId);
            if (allowance == null) return null;

            _context.Allowances.Remove(allowance);
            await SaveAsync();
            return allowance;
        }

        public async Task<Allowance> GetAllowanceByIdAsync(int allowanceId)
        {
            return await _context.Allowances.FindAsync(allowanceId);
        }

        public async Task<List<Allowance>> GetAllowancesByActiveAsync(bool active)
        {
            return await _context.Allowances
                .Where(x => x.Active == active)
                .ToListAsync();
        }

        public async Task<List<Allowance>> GetAllowancesAsync()
        {
            return await _context.Allowances.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Allowance> UpdateAllowanceAsync(Allowance allowance)
        {
            var allowanceUpdate = await GetAllowanceByIdAsync(allowance.Id);
            if (allowanceUpdate == null) return null;

            _context.Entry(allowanceUpdate).CurrentValues.SetValues(allowance);
            await SaveAsync();
            return allowanceUpdate;
        }

    }
}
