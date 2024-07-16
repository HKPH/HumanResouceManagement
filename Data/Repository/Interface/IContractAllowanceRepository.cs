using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IContractAllowanceRepository
    {
        List<Allowance> GetAllowancesByContract(int contractId);
        ContractAllowance GetContractAllowance(int contractId,int allowanceId);
        bool Save();
        bool CreateContractAllowance(ContractAllowance contractAllowance);
        bool UpdateContractAllowance(ContractAllowance contractAllowance);
        bool DeleteContractAllowance(int contractId, int allowanceId);

    }
}
