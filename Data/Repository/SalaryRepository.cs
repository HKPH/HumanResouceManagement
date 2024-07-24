using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public class SalaryRepository: ISalaryRepository
    {
        private readonly DBContext _context;
        public SalaryRepository(DBContext context) { _context = context; }

        public bool CreateSalary(Salary salary)
        {
            _context.Add(salary);
            return Save();
        }

        public bool DeleteSalary(int salaryId)
        {
            var salary=GetSalaryById(salaryId);
            _context.Remove(salary);
            return Save();
        }

        public List<Salary> GetSalaries()
        {
            return _context.Salaries.OrderBy(s=>s.Id).ToList();
        }

        public List<Salary> GetSalariesByActive(bool active)
        {
            return _context.Salaries.Where(s=>s.Active == active).ToList();
        }

        public List<Salary> GetSalariesNotUsing()
        {
            return _context.Salaries.Where(e=>e.EmployeeId!=null).ToList();
        }

        public Salary GetSalaryByEmployeeId(int employeeId)
        {
            return _context.Salaries.Where(e => e.EmployeeId == employeeId).FirstOrDefault();
        }

        public Salary GetSalaryById(int salaryId)
        {
            return _context.Salaries.FirstOrDefault(s => s.Id == salaryId);
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateSalary(Salary salary)
        {
            var salaryUpdate=GetSalaryById(salary.Id);
            _context.Entry(salaryUpdate).CurrentValues.SetValues(salary);
            return Save();
        }
    }
}
