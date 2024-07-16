using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class DepartmentJobTitleRepository:IDepartmentJobTitleRepository
    {
        private readonly DBContext _context;
        public DepartmentJobTitleRepository(DBContext context)
        {
            _context = context;
        }

        public bool CreateDepartmentJobTitle(DepartmentJobTitle departmentJobTitle)
        {
            _context.Add(departmentJobTitle);
            return Save();
        }

        public bool DeleteDepartmentJobTitle(int departmentId, int jobTitleId)
        {
            var dj=GetDepartmentJobTitle(departmentId, jobTitleId);
            _context.Remove(dj);
            return Save();

        }

        public DepartmentJobTitle GetDepartmentJobTitle(int departmentId, int jobTitleId)
        {
            return _context.DepartmentJobTitles.Where(dj => dj.DepartmentId == departmentId && dj.JobTitleId == jobTitleId).FirstOrDefault();
        }

        public List<DepartmentJobTitle> GetDepartmentJobTitlesByActive(bool active)
        {
            throw new NotImplementedException();
        }

        public List<JobTitle> GetJobtitlesByDepartment(int departmentId)
        {
            return _context.DepartmentJobTitles
                .Where(dj=>dj.DepartmentId == departmentId)
                .Include(dj=>dj.JobTitle)
                .Select(dj=>dj.JobTitle)
                .ToList();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateDepartmentJobTitle(DepartmentJobTitle departmentJobTitle)
        {
            var djUpdate = GetDepartmentJobTitle(departmentJobTitle.DepartmentId, departmentJobTitle.JobTitleId);
            _context.Entry(djUpdate).CurrentValues.SetValues(departmentJobTitle);
            return Save();
        }
    }
}
