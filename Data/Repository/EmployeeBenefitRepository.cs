using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class EmployeeBenefitRepository: IEmployeeBenefitRepository
    {
        private readonly DBContext _context;
        public EmployeeBenefitRepository(DBContext context)
        {
            _context = context;
        }
        public bool CreateEmployeeBenefit(EmployeeBenefit employeeBenefit)
        {
            _context.EmployeeBenefits.Add(employeeBenefit);
            return Save();
        }

        public bool DeleteEmployeeBenefit(int employeeId, int benefitId)
        {
            var employeeBenefit = GetEmployeeBenefit(employeeId, benefitId);
            _context.Remove(employeeBenefit);
            return Save();
        }

        public List<Benefit> GetBenefitsByEmployee(int employeeId)
        {
            return _context.EmployeeBenefits
                .Where(ca => ca.EmployeeId == employeeId)
                .Include(ca => ca.Benefit)
                .Select(ca => ca.Benefit)
                .ToList();
        }

        public EmployeeBenefit GetEmployeeBenefit(int employeeId, int benefitId)
        {
            return _context.EmployeeBenefits
                .Where(c => c.BenefitId == benefitId && c.EmployeeId == employeeId)
                .FirstOrDefault();

        }

        public bool Save()
        {
            var check = _context.SaveChanges();
            return check > 0 ? true : false;
        }

        public bool UpdateEmployeeBenefit(EmployeeBenefit employeeBenefit)
        {
            var ebUpdate = GetEmployeeBenefit(employeeBenefit.EmployeeId, employeeBenefit.BenefitId);
            _context.Entry(ebUpdate).CurrentValues.SetValues(employeeBenefit);
            return Save();
        }
    }
}
