using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class ResignationRepository : IResignationRepository
    {
        private readonly DBContext _context;

        public ResignationRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Resignation> CreateResignationAsync(Resignation resignation)
        {
            await _context.Resignations.AddAsync(resignation);
            await _context.SaveChangesAsync();;
            return resignation;
        }

        public async Task<Resignation> DeleteResignationAsync(int resignationId)
        {
            var resignation = await GetResignationByIdAsync(resignationId);
            if (resignation == null)
            {
                return null;
            }
            _context.Resignations.Remove(resignation);
            await _context.SaveChangesAsync();;
            return resignation;
        }

        public async Task<Resignation> GetResignationByIdAsync(int resignationId)
        {
            return await _context.Resignations
                .Where(r => r.Id == resignationId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Resignation>> GetResignationsAsync()
        {
            return await _context.Resignations
                .OrderBy(r => r.Id)
                .ToListAsync();
        }

        public async Task<List<Resignation>> GetResignationsByAcceptedAsync(bool accepted)
        {
            return await _context.Resignations
                .Where(r => r.Accepted == accepted)
                .ToListAsync();
        }

        public async Task<Resignation> UpdateResignationAsync(Resignation resignation)
        {
            var resignationUpdate = await GetResignationByIdAsync(resignation.Id);
            if (resignationUpdate == null)
            {
                return null;
            }
            _context.Entry(resignationUpdate).CurrentValues.SetValues(resignation);
            await _context.SaveChangesAsync();;
            return resignationUpdate;
        }
    }
}
