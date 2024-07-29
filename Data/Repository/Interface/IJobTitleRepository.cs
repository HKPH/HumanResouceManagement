using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IJobTitleRepository
    {
        Task<List<JobTitle>> GetJobTitlesAsync();
        Task<List<JobTitle>> GetJobTitlesByActiveAsync(bool active);
        Task<JobTitle> GetJobTitleByIdAsync(int jobTitleId);
        Task<JobTitle> CreateJobTitleAsync(JobTitle jobTitle);
        Task<JobTitle> UpdateJobTitleAsync(JobTitle jobTitle);
        Task<JobTitle> DeleteJobTitleAsync(int jobTitleId);
    }
}
