using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeFamilyRepository
    {
        Task<List<EmployeeFamily>> GetEmployeeFamiliesAsync();
        Task<List<EmployeeFamily>> GetEmployeeFamiliesByEmployeeIdAsync(int employeeId);
        Task<EmployeeFamily> GetEmployeeFamilyByIdAsync(int employeeFamilyId);
        Task<EmployeeFamily> CreateEmployeeFamilyAsync(EmployeeFamily employeeFamily);
        Task<EmployeeFamily> UpdateEmployeeFamilyAsync(EmployeeFamily employeeFamily);
        Task<EmployeeFamily> DeleteEmployeeFamilyAsync(int employeeFamilyId);
    }
}
