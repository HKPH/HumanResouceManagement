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
        public bool CreateEmployeeAllowance(EmployeeAllowance employeeAllowance)
        {
            _context.EmployeeAllowances.Add(employeeAllowance);
            return Save();
        }

        public bool DeleteEmployeeAllowance(int employeeId, int allowanceId)
        {
            var employeeAllowance = GetEmployeeAllowance(employeeId, allowanceId);
            _context.Remove(employeeAllowance);
            return Save();
        }

        public List<Allowance> GetAllowancesByEmployee(int employeeId)
        {
            return _context.EmployeeAllowances
                .Where(ca => ca.EmployeeId == employeeId)
                .Include(ca => ca.Allowance)
                .Select(ca => ca.Allowance)
                .ToList();
        }

        public EmployeeAllowance GetEmployeeAllowance(int employeeId, int allowanceId)
        {
            return _context.EmployeeAllowances
                .Where(c => c.AllowanceId == allowanceId && c.EmployeeId == employeeId)
                .FirstOrDefault();

        }

        public bool Save()
        {
            var check = _context.SaveChanges();
            return check > 0 ? true : false;
        }

        public bool UpdateEmployeeAllowance(EmployeeAllowance employeeAllowance)
        {
            var eaUpdate = GetEmployeeAllowance(employeeAllowance.EmployeeId, employeeAllowance.AllowanceId);
            _context.Entry(eaUpdate).CurrentValues.SetValues(employeeAllowance);
            return Save();
        }
    }
}
