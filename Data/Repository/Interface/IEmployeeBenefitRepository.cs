using HumanManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeBenefitRepository
    {
        Task<List<Benefit>> GetBenefitsByEmployeeAsync(int employeeId);
        Task<EmployeeBenefit> GetEmployeeBenefitAsync(int employeeId, int benefitId);
        Task<EmployeeBenefit> CreateEmployeeBenefitAsync(EmployeeBenefit employeeBenefit);
        Task<EmployeeBenefit> UpdateEmployeeBenefitAsync(EmployeeBenefit employeeBenefit);
        Task<EmployeeBenefit> DeleteEmployeeBenefitAsync(int employeeId, int benefitId);
    }
}
