using HumanManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IContractBenefitRepository
    {
        Task<List<Benefit>> GetBenefitsByContractAsync(int contractId);
        Task<ContractBenefit> GetContractBenefitAsync(int contractId, int benefitId);
        Task<bool> SaveAsync();
        Task<ContractBenefit> CreateContractBenefitAsync(ContractBenefit contractBenefit);
        Task<ContractBenefit> UpdateContractBenefitAsync(ContractBenefit contractBenefit);
        Task<ContractBenefit> DeleteContractBenefitAsync(int contractId, int benefitId);
    }
}
