using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeAllowanceRepository
    {
        List<Allowance> GetAllowancesByEmployee(int employeeId);
        EmployeeAllowance GetEmployeeAllowance(int employeeId, int allowanceId);
        bool Save();
        bool CreateEmployeeAllowance(EmployeeAllowance employeeAllowance);
        bool UpdateEmployeeAllowance(EmployeeAllowance employeeAllowance);
        bool DeleteEmployeeAllowance(int employeeId, int allowanceId);

    }
}
