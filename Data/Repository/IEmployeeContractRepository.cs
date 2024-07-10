using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public interface IEmployeeContractRepository
    {
        ICollection<EmployeeContract> GetEmployeeContracts();
        EmployeeContract GetEmployeeContractById(int employeeContractId);
        ICollection<EmployeeContract> GetEmployeeContractByActive(bool active);
        bool Save();
        bool CreateEmployeeContract(EmployeeContract employeeContract);
        bool UpdateEmployeeContract(EmployeeContract employeeContract);
        bool DeleteEmployeeContract(EmployeeContract employeeContract);
    }
}
