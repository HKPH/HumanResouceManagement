using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class JobTitleRepository : IJobTitleRepository
    {
        private readonly DBContext _context;

        public JobTitleRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<JobTitle> CreateJobTitleAsync(JobTitle jobTitle)
        {
            await _context.JobTitles.AddAsync(jobTitle);
            await _context.SaveChangesAsync();
            return jobTitle;
        }

        public async Task<JobTitle> DeleteJobTitleAsync(int jobTitleId)
        {
            var jobTitle = await GetJobTitleByIdAsync(jobTitleId);
            if (jobTitle == null)
            {
                return null;
            }

            _context.JobTitles.Remove(jobTitle);
            await _context.SaveChangesAsync();
            return jobTitle;
        }

        public async Task<List<JobTitle>> GetJobTitlesByActiveAsync(bool active)
        {
            return await _context.JobTitles.Where(j => j.Active == active).ToListAsync();
        }

        public async Task<JobTitle> GetJobTitleByIdAsync(int jobTitleId)
        {
            return await _context.JobTitles.FirstOrDefaultAsync(j => j.Id == jobTitleId);
        }

        public async Task<List<JobTitle>> GetJobTitlesAsync()
        {
            return await _context.JobTitles.OrderBy(j => j.Id).ToListAsync();
        }

        public async Task<JobTitle> UpdateJobTitleAsync(JobTitle jobTitle)
        {
            var jobTitleUpdate = await GetJobTitleByIdAsync(jobTitle.Id);
            if (jobTitleUpdate == null)
            {
                return null;
            }

            _context.Entry(jobTitleUpdate).CurrentValues.SetValues(jobTitle);
            await _context.SaveChangesAsync();
            return jobTitleUpdate;
        }
    }
}
