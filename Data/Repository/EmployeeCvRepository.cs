using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class EmployeeCvRepository : IEmployeeCvRepository
    {
        private readonly DBContext _context;

        public EmployeeCvRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<EmployeeCv> CreateEmployeeCvAsync(EmployeeCv employeeCv)
        {
            await _context.EmployeeCvs.AddAsync(employeeCv);
            await _context.SaveChangesAsync();
            return employeeCv;
        }

        public async Task<EmployeeCv> DeleteEmployeeCvAsync(int employeeCvId)
        {
            var employeeCv = await GetEmployeeCvByIdAsync(employeeCvId);
            if (employeeCv == null)
            {
                return null;
            }
            _context.EmployeeCvs.Remove(employeeCv);
            await _context.SaveChangesAsync();
            return employeeCv;
        }

        public async Task<List<EmployeeCv>> GetEmployeeCvsAsync()
        {
            return await _context.EmployeeCvs
                .OrderBy(cv => cv.Id)
                .ToListAsync();
        }

        public async Task<List<EmployeeCv>> GetEmployeeCvsByActiveAsync(bool active)
        {
            return await _context.EmployeeCvs
                .Where(cv => cv.Active == active)
                .ToListAsync();
        }

        public async Task<EmployeeCv> GetEmployeeCvByIdAsync(int employeeCvId)
        {
            return await _context.EmployeeCvs
                .Where(cv => cv.Id == employeeCvId)
                .FirstOrDefaultAsync();
        }

        public async Task<EmployeeCv> UpdateEmployeeCvAsync(EmployeeCv employeeCv)
        {
            var employeeCvUpdate = await GetEmployeeCvByIdAsync(employeeCv.Id);
            if (employeeCvUpdate == null)
            {
                return null;
            }
            _context.Entry(employeeCvUpdate).CurrentValues.SetValues(employeeCv);
            await _context.SaveChangesAsync();
            return employeeCvUpdate;
        }
    }
}
