using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public class EmployeeFamilyRepository:IEmployeeFamilyRepository
    {
        private readonly DBContext _context;
        public EmployeeFamilyRepository(DBContext context)
        {
            _context = context;
        }

        public bool CreateEmployeeFamily(EmployeeFamily employeeFamily)
        {
            _context.Add(employeeFamily);
            return Save();
        }

        public bool DeleteEmployeeFamily(int employeeFamilyId)
        {
            var ef=GetEmployeeFamilyById(employeeFamilyId);
            _context.Remove(ef);
            return Save();
        }

        public EmployeeFamily GetEmployeeFamilyById(int employeeFamilyId)
        {
            return _context.EmployeeFamilies.Where(ef => ef.Id == employeeFamilyId).FirstOrDefault();
        }

        public List<EmployeeFamily> GetEmployeeFamilies()
        {
            return _context.EmployeeFamilies.OrderBy(ef => ef.Id).ToList();
        }

        public List<EmployeeFamily> GetEmployeeFamiliesByEmployeeId(int employeeId)
        {
            return _context.EmployeeFamilies.Where(e=>e.EmployeeId == employeeId).ToList();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateEmployeeFamily(EmployeeFamily employeeFamily)
        {
            var efUpdate=GetEmployeeFamilyById(employeeFamily.Id);
            _context.Entry(efUpdate).CurrentValues.SetValues(employeeFamily);
            return Save();
        }
    }
}
