using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public class DisciplineRepository : IDisciplineRepository
    {
        private readonly DBContext _context;
        public DisciplineRepository(DBContext context)
        {
            _context = context;
        }

        public bool CreateDiscipline(Discipline discipline)
        {
            _context.Add(discipline);
            return Save();
        }

        public bool DeleteDiscipline(int disciplineId)
        {
            var discipline = GetDisciplineById(disciplineId);
            _context.Remove(discipline);
            return Save();
        }

        public Discipline GetDisciplineById(int disciplineId)
        {
            return _context.Disciplines.Where(d => d.Id == disciplineId).FirstOrDefault();
        }

        public List<Discipline> GetDisciplines()
        {
            return _context.Disciplines.OrderBy(d => d.Id).ToList();
        }

        public List<Discipline> GetDisciplinesByEmployeeId(int employeeId)
        {
            return _context.Disciplines.Where(d=>d.EmployeeId == employeeId).ToList();
        }

        public bool Save()
        {
            var check = _context.SaveChanges();
            return check > 0 ? true : false;
        }

        public bool UpdateDiscipline(Discipline discipline)
        {
            var disciplineUpdate = GetDisciplineById(discipline.Id);
            _context.Entry(disciplineUpdate).CurrentValues.SetValues(discipline);
            return Save();
        }
    }
}
