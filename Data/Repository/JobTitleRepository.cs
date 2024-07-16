using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public class JobTitleRepository : IJobTitleRepository
    {
        DBContext _context;
        public JobTitleRepository(DBContext context) { _context = context; }
        public bool CreateJobTitle(JobTitle jobTitle)
        {
            _context.Add(jobTitle);
            return Save();
        }

        public bool DeleteJobTitle(int jobTitleId)
        {
            var jobTitle = GetJobTitleById(jobTitleId);
            _context.Remove(jobTitle);
            return Save();
        }

        public List<JobTitle> GetJobTitlesByActive(bool active)
        {
            return _context.JobTitles.Where(j=> j.Active== active).ToList();
        }

        public JobTitle GetJobTitleById(int jobTitleId)
        {
            return _context.JobTitles.Where(j => j.Id == jobTitleId).FirstOrDefault();
        }

        public List<JobTitle> GetJobTitles()
        {
            return _context.JobTitles.OrderBy(j=>j.Id).ToList();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateJobTitle(JobTitle jobTitle)
        {
            var jobTitleUpdate=GetJobTitleById(jobTitle.Id);
            _context.Entry(jobTitleUpdate).CurrentValues.SetValues(jobTitle);
            return Save();
        }
    }
}
