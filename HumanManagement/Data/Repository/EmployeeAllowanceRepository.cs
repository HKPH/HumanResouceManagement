using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class EmployeeAllowanceRepository : IEmployeeAllowanceRepository
    {
        private readonly DBContext _context;

        public EmployeeAllowanceRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<EmployeeAllowance> CreateEmployeeAllowanceAsync(EmployeeAllowance employeeAllowance)
        {
            await _context.EmployeeAllowances.AddAsync(employeeAllowance);
            await _context.SaveChangesAsync();
            return employeeAllowance;
        }

        public async Task<EmployeeAllowance> DeleteEmployeeAllowanceAsync(int employeeId, int allowanceId)
        {
            var employeeAllowance = await GetEmployeeAllowanceAsync(employeeId, allowanceId);
            if (employeeAllowance == null)
            {
                return null;
            }
            _context.EmployeeAllowances.Remove(employeeAllowance);
            await _context.SaveChangesAsync();
            return employeeAllowance;
        }

        public async Task<List<Allowance>> GetAllowancesByEmployeeAsync(int employeeId)
        {
            return await _context.EmployeeAllowances
                .Where(ca => ca.EmployeeId == employeeId)
                .Include(ca => ca.Allowance)
                .Select(ca => ca.Allowance)
                .ToListAsync();
        }

        public async Task<EmployeeAllowance> GetEmployeeAllowanceAsync(int employeeId, int allowanceId)
        {
            return await _context.EmployeeAllowances
                .FirstOrDefaultAsync(c => c.AllowanceId == allowanceId && c.EmployeeId == employeeId);
        }

        public async Task<EmployeeAllowance> UpdateEmployeeAllowanceAsync(EmployeeAllowance employeeAllowance)
        {
            var eaUpdate = await GetEmployeeAllowanceAsync(employeeAllowance.EmployeeId, employeeAllowance.AllowanceId);
            if (eaUpdate == null)
            {
                return null;
            }
            _context.Entry(eaUpdate).CurrentValues.SetValues(employeeAllowance);
            await _context.SaveChangesAsync();
            return employeeAllowance;
        }
    }
}
