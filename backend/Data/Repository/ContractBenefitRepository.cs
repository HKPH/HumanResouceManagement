using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class ContractBenefitRepository : IContractBenefitRepository
    {
        private readonly DBContext _context;

        public ContractBenefitRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<ContractBenefit> CreateContractBenefitAsync(ContractBenefit contractBenefit)
        {
            await _context.ContractBenefits.AddAsync(contractBenefit);
            await SaveAsync();
            return contractBenefit;
        }

        public async Task<ContractBenefit> DeleteContractBenefitAsync(int contractId, int benefitId)
        {
            var contractBenefit = await GetContractBenefitAsync(contractId, benefitId);
            if (contractBenefit == null) return null;

            _context.ContractBenefits.Remove(contractBenefit);
            await SaveAsync();
            return contractBenefit;
        }

        public async Task<List<Benefit>> GetBenefitsByContractAsync(int contractId)
        {
            return await _context.ContractBenefits
                .Where(cb => cb.ContractTypeId == contractId)
                .Include(cb => cb.Benefit)
                .Select(cb => cb.Benefit)
                .ToListAsync();
        }

        public async Task<ContractBenefit> GetContractBenefitAsync(int contractId, int benefitId)
        {
            return await _context.ContractBenefits
                .Where(cb => cb.ContractTypeId == contractId && cb.BenefitId == benefitId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ContractBenefit> UpdateContractBenefitAsync(ContractBenefit contractBenefit)
        {
            var cbUpdate = await GetContractBenefitAsync(contractBenefit.ContractTypeId, contractBenefit.BenefitId);
            if (cbUpdate == null) return null;

            _context.Entry(cbUpdate).CurrentValues.SetValues(contractBenefit);
            await SaveAsync();
            return contractBenefit;
        }
    }
}
