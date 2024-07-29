using Microsoft.EntityFrameworkCore;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public class ContractAllowanceRepository : IContractAllowanceRepository
    {
        private readonly DBContext _context;

        public ContractAllowanceRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<ContractAllowance> CreateContractAllowanceAsync(ContractAllowance contractAllowance)
        {
            await _context.ContractAllowances.AddAsync(contractAllowance);
            await SaveAsync();
            return contractAllowance;
        }

        public async Task<ContractAllowance> DeleteContractAllowanceAsync(int contractId, int allowanceId)
        {
            var contractAllowance = await GetContractAllowanceAsync(contractId, allowanceId);
            if (contractAllowance == null)
                return null;

            _context.ContractAllowances.Remove(contractAllowance);
            await SaveAsync();
            return contractAllowance;
        }

        public async Task<List<Allowance>> GetAllowancesByContractAsync(int contractId)
        {
            return await _context.ContractAllowances
                .Where(ca => ca.ContractTypeId == contractId)
                .Include(ca => ca.Allowance)
                .Select(ca => ca.Allowance)
                .ToListAsync();
        }

        public async Task<ContractAllowance> GetContractAllowanceAsync(int contractId, int allowanceId)
        {
            return await _context.ContractAllowances
                .Where(c => c.AllowanceId == allowanceId && c.ContractTypeId == contractId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ContractAllowance> UpdateContractAllowanceAsync(ContractAllowance contractAllowance)
        {
            var caUpdate = await GetContractAllowanceAsync(contractAllowance.ContractTypeId, contractAllowance.AllowanceId);
            if (caUpdate == null)
                return null;

            _context.Entry(caUpdate).CurrentValues.SetValues(contractAllowance);
            await SaveAsync();
            return contractAllowance;
        }
    }
}
