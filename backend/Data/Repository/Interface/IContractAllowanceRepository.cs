using System.Collections.Generic;
using System.Threading.Tasks;
using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IContractAllowanceRepository
    {
        Task<List<Allowance>> GetAllowancesByContractAsync(int contractId);
        Task<ContractAllowance> GetContractAllowanceAsync(int contractId, int allowanceId);
        Task<bool> SaveAsync();
        Task<ContractAllowance> CreateContractAllowanceAsync(ContractAllowance contractAllowance);
        Task<ContractAllowance> UpdateContractAllowanceAsync(ContractAllowance contractAllowance);
        Task<ContractAllowance> DeleteContractAllowanceAsync(int contractId, int allowanceId);
    }
}
