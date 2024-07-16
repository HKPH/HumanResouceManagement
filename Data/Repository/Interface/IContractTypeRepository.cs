using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IContractTypeRepository
    {
        List<ContractType> GetContractTypes();
        ContractType GetContractTypeById(int contractTypeId);
        List<ContractType> GetContractTypesByActive(bool active);
        bool Save();
        bool CreateContractType(ContractType contractType);
        bool UpdateContractType(ContractType contractType);
        bool DeleteContractType(int contractTypeId);
    }
}
