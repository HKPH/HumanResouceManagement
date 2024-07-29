using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class EmployeeBenefitRepository : IEmployeeBenefitRepository
    {
        private readonly DBContext _context;

        public EmployeeBenefitRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<EmployeeBenefit> CreateEmployeeBenefitAsync(EmployeeBenefit employeeBenefit)
        {
            _context.EmployeeBenefits.Add(employeeBenefit);
            await _context.SaveChangesAsync();;
            return employeeBenefit;
        }

        public async Task<EmployeeBenefit> DeleteEmployeeBenefitAsync(int employeeId, int benefitId)
        {
            var employeeBenefit = await GetEmployeeBenefitAsync(employeeId, benefitId);
            if (employeeBenefit == null)
            {
                return null;
            }
            _context.EmployeeBenefits.Remove(employeeBenefit);
            await _context.SaveChangesAsync();
            return employeeBenefit;
        }

        public async Task<List<Benefit>> GetBenefitsByEmployeeAsync(int employeeId)
        {
            return await _context.EmployeeBenefits
                .Where(ca => ca.EmployeeId == employeeId)
                .Include(ca => ca.Benefit)
                .Select(ca => ca.Benefit)
                .ToListAsync();
        }

        public async Task<EmployeeBenefit> GetEmployeeBenefitAsync(int employeeId, int benefitId)
        {
            return await _context.EmployeeBenefits
                .Where(c => c.BenefitId == benefitId && c.EmployeeId == employeeId)
                .FirstOrDefaultAsync();
        }

        public async Task<EmployeeBenefit> UpdateEmployeeBenefitAsync(EmployeeBenefit employeeBenefit)
        {
            var ebUpdate = await GetEmployeeBenefitAsync(employeeBenefit.EmployeeId, employeeBenefit.BenefitId);
            if (ebUpdate == null)
            {
                return null;
            }
            _context.Entry(ebUpdate).CurrentValues.SetValues(employeeBenefit);
            await _context.SaveChangesAsync();
            return employeeBenefit;
        }
    }
}
