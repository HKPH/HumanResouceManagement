using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class EmployeeFamilyRepository : IEmployeeFamilyRepository
    {
        private readonly DBContext _context;

        public EmployeeFamilyRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeFamily>> GetEmployeeFamiliesAsync()
        {
            return await _context.EmployeeFamilies.OrderBy(ef => ef.Id).ToListAsync();
        }

        public async Task<List<EmployeeFamily>> GetEmployeeFamiliesByEmployeeIdAsync(int employeeId)
        {
            return await _context.EmployeeFamilies.Where(ef => ef.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<EmployeeFamily> GetEmployeeFamilyByIdAsync(int employeeFamilyId)
        {
            return await _context.EmployeeFamilies.FindAsync(employeeFamilyId);
        }

        public async Task<EmployeeFamily> CreateEmployeeFamilyAsync(EmployeeFamily employeeFamily)
        {
            await _context.EmployeeFamilies.AddAsync(employeeFamily);
            await _context.SaveChangesAsync();
            return employeeFamily;
        }

        public async Task<EmployeeFamily> UpdateEmployeeFamilyAsync(EmployeeFamily employeeFamily)
        {
            var employeeFamilyUpdate = await GetEmployeeFamilyByIdAsync(employeeFamily.Id);
            if (employeeFamilyUpdate == null)
            {
                return null;
            }

            _context.Entry(employeeFamilyUpdate).CurrentValues.SetValues(employeeFamily);
            await _context.SaveChangesAsync();
            return employeeFamilyUpdate;
        }

        public async Task<EmployeeFamily> DeleteEmployeeFamilyAsync(int employeeFamilyId)
        {
            var employeeFamily = await GetEmployeeFamilyByIdAsync(employeeFamilyId);
            if (employeeFamily == null)
            {
                return null;
            }

            _context.EmployeeFamilies.Remove(employeeFamily);
            await _context.SaveChangesAsync();
            return employeeFamily;
        }
    }
}
