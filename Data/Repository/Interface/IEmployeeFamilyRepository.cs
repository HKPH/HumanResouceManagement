using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeFamilyRepository
    {
        List<EmployeeFamily> GetEmployeeFamilys();
        EmployeeFamily GetEmployeeFamilyById(int employeeFamilyId);
        bool Save();
        bool CreateEmployeeFamily(EmployeeFamily employeeFamily);
        bool UpdateEmployeeFamily(EmployeeFamily employeeFamily);
        bool DeleteEmployeeFamily(int employeeFamilyId);
    }
}
