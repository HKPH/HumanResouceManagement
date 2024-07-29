using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DBContext _context;

        public EmployeeRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> DeleteEmployeeAsync(int employeeId)
        {
            var employee = await GetEmployeeByIdAsync(employeeId);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _context.Employees
                .Where(e => e.Id == employeeId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees
                .OrderBy(e => e.Id)
                .ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeesBirthdayAsync(int days)
        {
            DateTime today = DateTime.Now.Date;
            DateTime startDate = today.AddDays(-days);

            var startMonthDay = new DateTime(today.Year, startDate.Month, startDate.Day);
            var endMonthDay = new DateTime(today.Year, today.Month, today.Day);

            return await _context.Employees
                .Where(e => e.Dob.Month == startMonthDay.Month && e.Dob.Day >= startMonthDay.Day ||
                            e.Dob.Month == endMonthDay.Month && e.Dob.Day <= endMonthDay.Day)
                .ToListAsync();
        }


        public async Task<List<Employee>> GetEmployeesByActiveAsync(bool active)
        {
            return await _context.Employees
                .Where(e => e.Active == active)
                .ToListAsync();
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            var employeeUpdate = await GetEmployeeByIdAsync(employee.Id);
            _context.Entry(employeeUpdate).CurrentValues.SetValues(employee);
            await _context.SaveChangesAsync();
            return employeeUpdate;
        }

    }
}
