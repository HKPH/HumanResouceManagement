using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployees();
        List<Employee> GetEmployeesByActive(bool active);
        Employee GetEmployeeById(int employeeId);
        bool CreateEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(int employeeId);
        bool Save();
    }
}
