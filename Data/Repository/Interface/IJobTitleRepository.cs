using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IJobTitleRepository
    {
        List<JobTitle> GetJobTitles();
        List<JobTitle> GetJobTitlesByActive(bool active);
        JobTitle GetJobTitleById(int jobTitleId);
        bool Save();
        bool CreateJobTitle(JobTitle jobTitle);
        bool UpdateJobTitle(JobTitle jobTitle);
        bool DeleteJobTitle(int jobTitleId);
    }
}
