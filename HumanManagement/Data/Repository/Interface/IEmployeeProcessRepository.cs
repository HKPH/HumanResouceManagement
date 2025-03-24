using HumanManagement.Models;
namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeProcessRepository
    {
        Task<List<EmployeeProcess>> GetEmployeeProcessesAsync();
        Task<EmployeeProcess> GetEmployeeProcessByIdAsync(int employeeProcessId);
        Task<List<EmployeeProcess>> GetEmployeeProcessesByActiveAsync(bool active);
        Task<List<EmployeeProcess>> GetEmployeeProcessesByEmployeeIdAsync(int employeeId);
        Task<EmployeeProcess> CreateEmployeeProcessAsync(EmployeeProcess employeeProcess);
        Task<EmployeeProcess> UpdateEmployeeProcessAsync(EmployeeProcess employeeProcess);
        Task<EmployeeProcess> DeleteEmployeeProcessAsync(int employeeProcessId);
    }
}
