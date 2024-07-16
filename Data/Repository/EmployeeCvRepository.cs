using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public class EmployeeCvRepository : IEmployeeCvRepository
    {
        private readonly DBContext _context;
        public EmployeeCvRepository(DBContext context)
        {
            _context = context;
        }
        public bool CreateEmployeeCv(EmployeeCv employeeCv)
        {
            _context.Add(employeeCv);
            return Save();
        }

        public bool DeleteEmployeeCv(int employeeCvId)
        {
            var employeeCv= GetEmployeeCvById(employeeCvId);
            _context.Remove(employeeCv);
            return Save();
        }

        public List<EmployeeCv> GetEmployeeCvs()
        {
            return _context.EmployeeCvs.OrderBy(cv=>cv.Id).ToList();
        }

        public List<EmployeeCv> GetEmployeeCvsByActive(bool active)
        {
            return _context.EmployeeCvs.Where(cv => cv.Active==active).ToList();
        }

        public EmployeeCv GetEmployeeCvById(int employeeCvId)
        {
            return _context.EmployeeCvs.Where(cv => cv.Id == employeeCvId).FirstOrDefault();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateEmployeeCv(EmployeeCv employeeCv)
        {
            var employeeCvUpdate = GetEmployeeCvById(employeeCv.Id);
            _context.Entry(employeeCvUpdate).CurrentValues.SetValues(employeeCv);
            return Save();

        }
    }
}
