using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanManagement.Data.Repository
{
    public class DepartmentJobTitleRepository : IDepartmentJobTitleRepository
    {
        private readonly DBContext _context;

        public DepartmentJobTitleRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<DepartmentJobTitle> CreateDepartmentJobTitleAsync(DepartmentJobTitle departmentJobTitle)
        {
            await _context.DepartmentJobTitles.AddAsync(departmentJobTitle);
            await _context.SaveChangesAsync();
            return departmentJobTitle;
        }

        public async Task<DepartmentJobTitle> DeleteDepartmentJobTitleAsync(int departmentId, int jobTitleId)
        {
            var departmentJobTitle = await GetDepartmentJobTitleAsync(departmentId, jobTitleId);
            if (departmentJobTitle == null)
            {
                return null;
            }

            _context.DepartmentJobTitles.Remove(departmentJobTitle);
            await _context.SaveChangesAsync();
            return departmentJobTitle;
        }

        public async Task<DepartmentJobTitle> GetDepartmentJobTitleAsync(int departmentId, int jobTitleId)
        {
            return await _context.DepartmentJobTitles
                .Where(dj => dj.DepartmentId == departmentId && dj.JobTitleId == jobTitleId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<DepartmentJobTitle>> GetDepartmentJobTitlesByActiveAsync(bool active)
        {
            // Implement the method if needed
            throw new NotImplementedException();
        }

        public async Task<List<JobTitle>> GetJobTitlesByDepartmentAsync(int departmentId)
        {
            return await _context.DepartmentJobTitles
                .Where(dj => dj.DepartmentId == departmentId)
                .Include(dj => dj.JobTitle)
                .Select(dj => dj.JobTitle)
                .ToListAsync();
        }

        public async Task<DepartmentJobTitle> UpdateDepartmentJobTitleAsync(DepartmentJobTitle departmentJobTitle)
        {
            var existingDepartmentJobTitle = await GetDepartmentJobTitleAsync(departmentJobTitle.DepartmentId, departmentJobTitle.JobTitleId);
            if (existingDepartmentJobTitle == null)
            {
                return null;
            }

            _context.Entry(existingDepartmentJobTitle).CurrentValues.SetValues(departmentJobTitle);
            await _context.SaveChangesAsync();
            return departmentJobTitle;
        }

    }
}
