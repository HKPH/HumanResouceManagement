using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public interface IEmployeeCvRepository
    {
        List<EmployeeCv> GetEmployeeCvs();
        EmployeeCv GetEmployeesCvById(int employeeCvId);
        List<EmployeeCv> GetEmployeesCvByActive(bool active);
        bool CreateEmployeeCv(EmployeeCv employeeCv);
        bool UpdateEmployeeCv(EmployeeCv employeeCv);
        bool DeleteEmployeeCv(EmployeeCv employeeCv);
        bool Save();

    }
}
