using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeCvRepository
    {
        Task<List<EmployeeCv>> GetEmployeeCvsAsync();
        Task<EmployeeCv> GetEmployeeCvByIdAsync(int employeeCvId);
        Task<List<EmployeeCv>> GetEmployeeCvsByActiveAsync(bool active);
        Task<EmployeeCv> CreateEmployeeCvAsync(EmployeeCv employeeCv);
        Task<EmployeeCv> UpdateEmployeeCvAsync(EmployeeCv employeeCv);
        Task<EmployeeCv> DeleteEmployeeCvAsync(int employeeCvId);
    }
}
