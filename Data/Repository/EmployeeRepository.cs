using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly DBContext _context;
        public EmployeeRepository(DBContext context)
        {
            _context = context;
        }

        public bool CreateEmployee(Employee employee)
        {
            _context.Add(employee);
            return Save();
        }

        public bool DeleteEmployee(int employeeId)
        {
            var employee=GetEmployeeById(employeeId);
            _context.Remove(employee);
            return Save();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _context.Employees.Where(e => e.Id == employeeId).FirstOrDefault();
        }

        public List<Employee> GetEmployees()
        {
            return _context.Employees.OrderBy(e=>e.Id).ToList();
        }

        public List<Employee> GetEmployeesBirthday(int days)
        {
            DateTime today = DateTime.Now.Date;
            DateTime startDate=today.AddDays(-days);
            return _context.Employees
                .AsEnumerable()
                .Where(e=>
                CheckDOB(startDate,e.Dob,today)==true)
            .ToList();
        }
        public bool CheckDOB(DateTime a, DateTime b, DateTime c)
        {
            
            DateTime a1 = new DateTime(1, a.Month, a.Day);
            DateTime b1 = new DateTime(1, b.Month, b.Day);
            DateTime c1 = new DateTime(1, c.Month, c.Day);
            return (b1 >= a1 && b1<= c1);
        }
        public List<Employee> GetEmployeesByActive(bool active)
        {
            return _context.Employees.Where(e=>e.Active==active).ToList();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateEmployee(Employee employee)
        {
            var employeeUpdate=GetEmployeeById(employee.Id);
            _context.Entry(employeeUpdate).CurrentValues.SetValues(employee);
            return Save();
        }
    }
}
