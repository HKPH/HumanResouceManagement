using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IContractBenefitRepository
    {
        List<Benefit> GetBenefitsByContract(int contractId);
        ContractBenefit GetContractBenefit(int contractId, int benefitId);
        bool Save();
        bool CreateContractBenefit(ContractBenefit contractBenefit);
        bool UpdateContractBenefit(ContractBenefit contractBenefit);
        bool DeleteContractBenefit(int contractId, int benefitId);
    }
}
