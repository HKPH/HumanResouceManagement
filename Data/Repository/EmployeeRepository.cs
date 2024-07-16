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
