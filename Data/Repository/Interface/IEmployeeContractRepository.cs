using HumanManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeContractRepository
    {
        Task<List<EmployeeContract>> GetEmployeeContractsAsync();
        Task<EmployeeContract> GetEmployeeContractByIdAsync(int employeeContractId);
        Task<List<EmployeeContract>> GetEmployeeContractsByActiveAsync(bool active);
        Task<EmployeeContract> CreateEmployeeContractAsync(EmployeeContract employeeContract);
        Task<EmployeeContract> UpdateEmployeeContractAsync(EmployeeContract employeeContract);
        Task<EmployeeContract> DeleteEmployeeContractAsync(int employeeContractId);
    }
}
