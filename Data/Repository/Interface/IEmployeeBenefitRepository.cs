using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeBenefitRepository
    {
        List<Benefit> GetBenefitsByEmployee(int employeeId);
        EmployeeBenefit GetEmployeeBenefit(int employeeId, int benefitId);
        bool Save();
        bool CreateEmployeeBenefit(EmployeeBenefit employeeBenefit);
        bool UpdateEmployeeBenefit(EmployeeBenefit employeeBenefit);
        bool DeleteEmployeeBenefit(int employeeId, int benefitId);
    }
}
