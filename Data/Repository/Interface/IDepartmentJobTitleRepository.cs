using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IDepartmentJobTitleRepository
    {
        List<DepartmentJobTitle> GetDepartmentJobTitlesByActive(bool active);
        
        List<JobTitle> GetJobtitlesByDepartment(int departmentId);
        DepartmentJobTitle GetDepartmentJobTitle(int departmentId, int jobTitleId);

        bool Save();
        bool CreateDepartmentJobTitle(DepartmentJobTitle departmentJobTitle);
        bool UpdateDepartmentJobTitle(DepartmentJobTitle departmentJobTitle);
        bool DeleteDepartmentJobTitle(int departmentId, int jobTitleId);
    }
}
