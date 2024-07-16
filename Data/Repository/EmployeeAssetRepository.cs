using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class EmployeeAssetRepository: IEmployeeAssetRepository
    {
        private readonly DBContext _context;
        public EmployeeAssetRepository(DBContext context)
        {
            _context = context;
        }

        public bool CreateEmployeeAsset(EmployeeAsset employeeAsset)
        {
            _context.Add(employeeAsset);
            return Save();
        }

        public bool DeleteEmployeeAsset(int employeeId, int assetId)
        {
            var ea=GetEmployeeAsset(employeeId, assetId);
            _context.Remove(ea);
            return Save();
        }

        public List<Asset> GetAssetsByEmployee(int employeeId)
        {
            return _context.EmployeeAssets
                .Where(ea=>ea.EmployeeId == employeeId)
                .Include(ea => ea.Asset)
                .Select(ea=>ea.Asset)
                .ToList();
        }

        public EmployeeAsset GetEmployeeAsset(int employeeId, int assetId)
        {
            return _context.EmployeeAssets.Where(ea =>ea.EmployeeId == employeeId && ea.AssetId == assetId).FirstOrDefault();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check > 0?true:false;
        }

        public bool UpdateEmployeeAsset(EmployeeAsset employeeAsset)
        {
            var eaUpdate = GetEmployeeAsset(employeeAsset.EmployeeId, employeeAsset.AssetId);
            _context.Entry(eaUpdate).CurrentValues.SetValues(employeeAsset);
            return Save();
        }
    }
}
