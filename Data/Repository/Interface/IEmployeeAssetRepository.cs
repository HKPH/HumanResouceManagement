using HumanManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeAssetRepository
    {
        Task<List<Asset>> GetAssetsByEmployeeAsync(int employeeId);
        Task<EmployeeAsset> GetEmployeeAssetAsync(int employeeId, int assetId);
        Task<EmployeeAsset> CreateEmployeeAssetAsync(EmployeeAsset employeeAsset);
        Task<EmployeeAsset> UpdateEmployeeAssetAsync(EmployeeAsset employeeAsset);
        Task<EmployeeAsset> DeleteEmployeeAssetAsync(int employeeId, int assetId);
    }
}
