using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly DBContext _context;
        public BankRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Bank> CreateBankAsync(Bank bank)
        {
            await _context.Banks.AddAsync(bank);
            await SaveAsync();
            return bank;
        }

        public async Task<Bank> GetBankByIdAsync(int bankId)
        {
            return await _context.Banks
                .FirstOrDefaultAsync(b => b.Id == bankId);
        }

        public async Task<List<Bank>> GetBanksAsync()
        {
            return await _context.Banks
                .OrderBy(b => b.Id)
                .ToListAsync();
        }

        public async Task<bool> HasBankAsync(int bankId)
        {
            return await _context.Banks
                .AnyAsync(b => b.Id == bankId);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<Bank> CheckBankByNameAsync(Bank bank)
        {
            return await _context.Banks
                .FirstOrDefaultAsync(c => c.Name.Trim().ToUpper() == bank.Name.TrimEnd().ToUpper());
        }

        public async Task<Bank> UpdateBankAsync(Bank bank)
        {
            var bankUpdate = await GetBankByIdAsync(bank.Id);
            _context.Entry(bankUpdate).CurrentValues.SetValues(bank);
            await SaveAsync();
            return bank;
        }

        public async Task<Bank> DeleteBankAsync(int bankId)
        {
            var bank = await GetBankByIdAsync(bankId);
            _context.Banks.Remove(bank);
            await SaveAsync();
            return bank;
        }

        public async Task<List<Bank>> GetBanksByActiveAsync(bool active)
        {
            return await _context.Banks
                .Where(b => b.Active == active)
                .OrderBy(b => b.Id)
                .ToListAsync();
        }

    }
}
