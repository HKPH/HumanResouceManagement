using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanManagement.Data.Repository
{
    public class DisciplineRepository : IDisciplineRepository
    {
        private readonly DBContext _context;
        public DisciplineRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Discipline> CreateDisciplineAsync(Discipline discipline)
        {
            await _context.Disciplines.AddAsync(discipline);
            await _context.SaveChangesAsync();;
            return discipline;
        }

        public async Task<Discipline> DeleteDisciplineAsync(int disciplineId)
        {
            var discipline = await GetDisciplineByIdAsync(disciplineId);
            if (discipline == null)
            {
                return null;
            }

            _context.Disciplines.Remove(discipline);
            await _context.SaveChangesAsync();;
            return discipline;
        }

        public async Task<Discipline> GetDisciplineByIdAsync(int disciplineId)
        {
            return await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == disciplineId);
        }

        public async Task<List<Discipline>> GetDisciplinesAsync()
        {
            return await _context.Disciplines.OrderBy(d => d.Id).ToListAsync();
        }

        public async Task<List<Discipline>> GetDisciplinesByEmployeeIdAsync(int employeeId)
        {
            return await _context.Disciplines.Where(d => d.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<Discipline> UpdateDisciplineAsync(Discipline discipline)
        {
            var disciplineUpdate = await GetDisciplineByIdAsync(discipline.Id);
            if (disciplineUpdate == null)
            {
                return null;
            }
            _context.Entry(disciplineUpdate).CurrentValues.SetValues(discipline);
            await _context.SaveChangesAsync();;
            return discipline;
        }
    }
}
