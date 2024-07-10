using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public class AllowanceRepository : IAllowanceRepository
    {
        private readonly DBContext _context;
        public AllowanceRepository(DBContext context)
        {
            _context = context;
        }
        public bool CreateAllowance(Allowance allowance)
        {
            _context.Add(allowance);
            return Save();
        }

        public bool DeleteAllowance(Allowance allowance)
        {
            _context.Remove(allowance);
            return Save();
        }

        public Allowance GetAllowanceById(int allowanceId)
        {
            return _context.Allowances.Where(x => x.Id == allowanceId).FirstOrDefault();
        }

        public ICollection<Allowance> GetAllowanceByActive(bool active)
        {
            return _context.Allowances.Where(x => x.Active == active).ToList();
        }

        public ICollection<Allowance> GetAllowances()
        {
            return _context.Allowances.ToList();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateAllowance(Allowance allowance)
        {
            var allowanceUpdate=GetAllowanceById(allowance.Id);
            _context.Entry(allowanceUpdate).CurrentValues.SetValues(allowance);
            return Save();

        }
    }
}
