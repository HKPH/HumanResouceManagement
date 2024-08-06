using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int departmentId);
        Task<List<Department>> GetDepartmentsByActiveAsync(bool active);
        Task<Department> CreateDepartmentAsync(Department department);
        Task<Department> UpdateDepartmentAsync(Department department);
        Task<Department> DeleteDepartmentAsync(int departmentId);
    }
}
