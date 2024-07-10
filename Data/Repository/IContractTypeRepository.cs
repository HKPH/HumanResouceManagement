using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public interface IContractTypeRepository
    {
        ICollection<ContractType> GetContractTypes();
        ContractType GetContractTypeById(int contractTypeId);
        ICollection<ContractType> GetContractTypeByActive(bool active);
        bool Save();
        bool CreateContractType(ContractType contractType);
        bool UpdateContractType(ContractType contractType);
        bool DeleteContractType(ContractType contractType);
    }
}
