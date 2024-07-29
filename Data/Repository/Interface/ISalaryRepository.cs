using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface ISalaryRepository
    {
        Task<List<Salary>> GetSalariesAsync();
        Task<Salary> GetSalaryByEmployeeIdAsync(int employeeId);
        Task<List<Salary>> GetSalariesNotUsingAsync();
        Task<List<Salary>> GetSalariesByActiveAsync(bool active);
        Task<Salary> GetSalaryByIdAsync(int salaryId);
        Task<Salary> CreateSalaryAsync(Salary salary);
        Task<Salary> UpdateSalaryAsync(Salary salary);
        Task<Salary> DeleteSalaryAsync(int salaryId);
    }
}
