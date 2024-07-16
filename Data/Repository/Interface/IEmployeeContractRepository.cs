using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeContractRepository
    {
        List<EmployeeContract> GetEmployeeContracts();
        EmployeeContract GetEmployeeContractById(int employeeContractId);
        List<EmployeeContract> GetEmployeeContractsByActive(bool active);
        bool Save();
        bool CreateEmployeeContract(EmployeeContract employeeContract);
        bool UpdateEmployeeContract(EmployeeContract employeeContract);
        bool DeleteEmployeeContract(int employeeContractId);
    }
}
