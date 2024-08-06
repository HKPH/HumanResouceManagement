using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeAllowanceRepository
    {
        Task<List<Allowance>> GetAllowancesByEmployeeAsync(int employeeId);
        Task<EmployeeAllowance> GetEmployeeAllowanceAsync(int employeeId, int allowanceId);
        Task<EmployeeAllowance> CreateEmployeeAllowanceAsync(EmployeeAllowance employeeAllowance);
        Task<EmployeeAllowance> UpdateEmployeeAllowanceAsync(EmployeeAllowance employeeAllowance);
        Task<EmployeeAllowance> DeleteEmployeeAllowanceAsync(int employeeId, int allowanceId);
    }
}
