using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public class ResignationRepository:IResignationRepository
    {
        private readonly DBContext _context;
        public ResignationRepository(DBContext context)
        {
            _context = context;
        }

        public bool CreateResignation(Resignation resignation)
        {
            _context.Add(resignation);
            return Save();
        }

        public bool DeleteResignation(int resignationId)
        {
            var resignation=GetResignationById(resignationId);
            _context.Remove(resignation);
            return Save();
        }
        public bool Save()
        {
            var check = _context.SaveChanges();
            return check > 0 ? true : false;
        }

        public Resignation GetResignationById(int resignationId)
        {
            return _context.Resignations.Where(r => r.Id == resignationId).FirstOrDefault();
        }

        public List<Resignation> GetResignations()
        {
            return _context.Resignations.OrderBy(r => r.Id).ToList();
        }

        public bool UpdateResignation(Resignation resignation)
        {
            var resignationUpdate=GetResignationById(resignation.Id);
            _context.Entry(resignationUpdate).CurrentValues.SetValues(resignation);
            return Save();
        }

        public List<Resignation> GetResignationsByAccepted(bool accepted)
        {
            return _context.Resignations.Where(r=>r.Accepted==accepted).ToList();
        }
    }
}
