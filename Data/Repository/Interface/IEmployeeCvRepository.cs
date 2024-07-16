using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeCvRepository
    {
        List<EmployeeCv> GetEmployeeCvs();
        EmployeeCv GetEmployeeCvById(int employeeCvId);
        List<EmployeeCv> GetEmployeeCvsByActive(bool active);
        bool CreateEmployeeCv(EmployeeCv employeeCv);
        bool UpdateEmployeeCv(EmployeeCv employeeCv);
        bool DeleteEmployeeCv(int employeeCvId);
        bool Save();

    }
}
