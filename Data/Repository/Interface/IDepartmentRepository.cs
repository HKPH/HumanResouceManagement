using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IDepartmentRepository
    {
        List<Department> GetDepartments();
        Department GetDepartmentById(int departmentId);
        List<Department> GetDepartmentsByActive(bool active);
        bool Save();
        bool CreateDepartment(Department department);
        bool UpdateDepartment(Department department);
        bool DeleteDepartment(int departmentId);
    }
}
