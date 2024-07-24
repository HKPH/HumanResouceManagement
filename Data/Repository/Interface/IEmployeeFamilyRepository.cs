using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeFamilyRepository
    {
        List<EmployeeFamily> GetEmployeeFamilies();
        List<EmployeeFamily> GetEmployeeFamiliesByEmployeeId(int employeeId);
        EmployeeFamily GetEmployeeFamilyById(int employeeFamilyId);
        bool Save();
        bool CreateEmployeeFamily(EmployeeFamily employeeFamily);
        bool UpdateEmployeeFamily(EmployeeFamily employeeFamily);
        bool DeleteEmployeeFamily(int employeeFamilyId);
    }
}
