using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HumanManagement.Data.Repository
{
    public class BankBranchRepository : IBankBranchRepository
    {
        private readonly DBContext _context;
        public BankBranchRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<BankBranch> CheckBankBranchByNameAsync(BankBranchDto bankBranch)
        {
            return await _context.BankBranches
                .FirstOrDefaultAsync(c => c.Name.Trim().ToUpper() == bankBranch.Name.TrimEnd().ToUpper());
        }

        public async Task<BankBranch> CreateBankBranchAsync(BankBranch bankBranch)
        {
            await _context.BankBranches.AddAsync(bankBranch);
            await SaveAsync();
            return bankBranch;
        }

        public async Task<BankBranch> DeleteBankBranchAsync(int bankBranchId)
        {
            var bankBranch = await GetBankBranchByIdAsync(bankBranchId);
            _context.BankBranches.Remove(bankBranch);
            await SaveAsync();
            return bankBranch;
        }

        public async Task<List<BankBranch>> GetAllBankBranchesByBankIdAsync(int bankId)
        {
            return await _context.BankBranches
                .Where(b => b.BankId == bankId)
                .ToListAsync();
        }

        public async Task<BankBranch> GetBankBranchByIdAsync(int bankBranchId)
        {
            return await _context.BankBranches
                .FirstOrDefaultAsync(b => b.Id == bankBranchId);
        }

        public async Task<List<BankBranch>> GetBankBranchesAsync()
        {
            return await _context.BankBranches
                .OrderBy(b => b.Id)
                .ToListAsync();
        }

        public async Task<bool> HasBankBranchAsync(int bankBranchId)
        {
            return await _context.BankBranches
                .AnyAsync(b => b.Id == bankBranchId);
        }

        public async Task<bool> SaveAsync()
        {
            var check = await _context.SaveChangesAsync();
            return check > 0;
        }

        public async Task<BankBranch> UpdateBankBranchAsync(BankBranch bankBranch)
        {
            var bankBranchUpdate = await GetBankBranchByIdAsync(bankBranch.Id);
            _context.Entry(bankBranchUpdate).CurrentValues.SetValues(bankBranch);
            await SaveAsync();
            return bankBranch;
        }

        public async Task<List<BankBranch>> GetBankBranchesByActiveAsync(bool active)
        {
            return await _context.BankBranches
                .Where(b => b.Active == active)
                .ToListAsync();
        }
    }
}
