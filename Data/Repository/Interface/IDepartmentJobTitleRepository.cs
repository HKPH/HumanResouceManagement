using HumanManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IDepartmentJobTitleRepository
    {
        Task<List<DepartmentJobTitle>> GetDepartmentJobTitlesByActiveAsync(bool active);
        Task<List<JobTitle>> GetJobTitlesByDepartmentAsync(int departmentId);
        Task<DepartmentJobTitle> GetDepartmentJobTitleAsync(int departmentId, int jobTitleId);
        Task<DepartmentJobTitle> CreateDepartmentJobTitleAsync(DepartmentJobTitle departmentJobTitle);
        Task<DepartmentJobTitle> UpdateDepartmentJobTitleAsync(DepartmentJobTitle departmentJobTitle);
        Task<DepartmentJobTitle> DeleteDepartmentJobTitleAsync(int departmentId, int jobTitleId);
    }
}
