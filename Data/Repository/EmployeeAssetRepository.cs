using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanManagement.Data.Repository
{
    public class EmployeeAssetRepository : IEmployeeAssetRepository
    {
        private readonly DBContext _context;

        public EmployeeAssetRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<EmployeeAsset> CreateEmployeeAssetAsync(EmployeeAsset employeeAsset)
        {
            await _context.EmployeeAssets.AddAsync(employeeAsset);
            await _context.SaveChangesAsync();;
            return employeeAsset;
        }

        public async Task<EmployeeAsset> DeleteEmployeeAssetAsync(int employeeId, int assetId)
        {
            var employeeAsset = await GetEmployeeAssetAsync(employeeId, assetId);
            if (employeeAsset == null)
            {
                return null;
            }

            _context.EmployeeAssets.Remove(employeeAsset);
            await _context.SaveChangesAsync();;
            return employeeAsset;
        }

        public async Task<List<Asset>> GetAssetsByEmployeeAsync(int employeeId)
        {
            return await _context.EmployeeAssets
                .Where(ea => ea.EmployeeId == employeeId)
                .Include(ea => ea.Asset)
                .Select(ea => ea.Asset)
                .ToListAsync();
        }

        public async Task<EmployeeAsset> GetEmployeeAssetAsync(int employeeId, int assetId)
        {
            return await _context.EmployeeAssets
                .Where(ea => ea.EmployeeId == employeeId && ea.AssetId == assetId)
                .FirstOrDefaultAsync();
        }

        public async Task<EmployeeAsset> UpdateEmployeeAssetAsync(EmployeeAsset employeeAsset)
        {
            var existingEmployeeAsset = await GetEmployeeAssetAsync(employeeAsset.EmployeeId, employeeAsset.AssetId);
            if (existingEmployeeAsset == null)
            {
                return null;
            }

            _context.Entry(existingEmployeeAsset).CurrentValues.SetValues(employeeAsset);
            await _context.SaveChangesAsync();;
            return employeeAsset;
        }
        public async Task<List<Asset>> GetBorrowedAssetsAsync()
        {
            return await _context.EmployeeAssets
                .Select(ea => ea.Asset)
                .ToListAsync();
        }

    }
}
