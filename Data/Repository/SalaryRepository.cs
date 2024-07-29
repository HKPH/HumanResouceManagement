using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly DBContext _context;

        public SalaryRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Salary> CreateSalaryAsync(Salary salary)
        {
            await _context.Salaries.AddAsync(salary);
            await _context.SaveChangesAsync();
            return salary;
        }

        public async Task<Salary> DeleteSalaryAsync(int salaryId)
        {
            var salary = await GetSalaryByIdAsync(salaryId);
            if (salary == null)
            {
                return null;
            }
            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();
            return salary;
        }

        public async Task<List<Salary>> GetSalariesAsync()
        {
            return await _context.Salaries.OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<List<Salary>> GetSalariesByActiveAsync(bool active)
        {
            return await _context.Salaries.Where(s => s.Active == active).ToListAsync();
        }

        public async Task<List<Salary>> GetSalariesNotUsingAsync()
        {
            return await _context.Salaries.Where(e => e.EmployeeId != null).ToListAsync();
        }

        public async Task<Salary> GetSalaryByEmployeeIdAsync(int employeeId)
        {
            return await _context.Salaries.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<Salary> GetSalaryByIdAsync(int salaryId)
        {
            return await _context.Salaries.FirstOrDefaultAsync(s => s.Id == salaryId);
        }

        public async Task<Salary> UpdateSalaryAsync(Salary salary)
        {
            var salaryToUpdate = await GetSalaryByIdAsync(salary.Id);
            if (salaryToUpdate == null)
            {
                return null;
            }
            _context.Entry(salaryToUpdate).CurrentValues.SetValues(salary);
            await _context.SaveChangesAsync();
            return salaryToUpdate;
        }
    }
}
