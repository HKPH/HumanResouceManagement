using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IContractTypeRepository
    {
        Task<List<ContractType>> GetContractTypesAsync();
        Task<ContractType> GetContractTypeByIdAsync(int contractTypeId);
        Task<List<ContractType>> GetContractTypesByActiveAsync(bool active);
        Task<bool> SaveAsync();
        Task<ContractType> CreateContractTypeAsync(ContractType contractType);
        Task<ContractType> UpdateContractTypeAsync(ContractType contractType);
        Task<ContractType> DeleteContractTypeAsync(int contractTypeId);

    }
}
