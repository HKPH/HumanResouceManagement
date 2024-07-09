using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public class HealthCareRepository : IHealthCareRepository
    {
        private readonly DBContext _context;
        public HealthCareRepository(DBContext context)
        {
            _context = context;
        }
        public bool CreateHealthCare(HealthCare healthCare)
        {
            _context.HealthCares.Add(healthCare);
            return Save();
        }

        public bool DeleteHealthCare(HealthCare healthCare)
        {
            _context.Remove(healthCare);
            return Save();
        }

        public List<HealthCare> GetHealthCareByActive(bool active)
        {
            return _context.HealthCares.Where(h=>h.Active == active).ToList(); 
        }

        public HealthCare GetHealthCareById(int healthCareId)
        {
            return _context.HealthCares.Where(h => h.Id == healthCareId).FirstOrDefault();
        }

        public List<HealthCare> GetHealthCares()
        {
            return _context.HealthCares.OrderBy(h=>h.Id).ToList();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateHealthCare(HealthCare healthCare)
        {
            _context.Update(healthCare);
            return Save();
        }
    }
}
