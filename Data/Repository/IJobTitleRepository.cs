using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public interface IJobTitleRepository
    {
        ICollection<JobTitle> GetJobTitles();
        ICollection<JobTitle> GetJobTitleByActive(bool active);
        JobTitle GetJobTitleById(int jobTitleId);
        bool Save();
        bool CreateJobTitle(JobTitle jobTitle);
        bool UpdateJobTitle(JobTitle jobTitle);
        bool DeleteJobTitle(JobTitle jobTitle);
    }
}
