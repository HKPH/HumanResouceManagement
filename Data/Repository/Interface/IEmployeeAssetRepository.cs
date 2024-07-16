using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeAssetRepository
    {
        List<Asset> GetAssetsByEmployee(int employeeId);
        EmployeeAsset GetEmployeeAsset(int employeeId, int assetId);
        bool Save();
        bool CreateEmployeeAsset(EmployeeAsset employeeAsset);
        bool UpdateEmployeeAsset(EmployeeAsset employeeAsset);
        bool DeleteEmployeeAsset(int employeeId, int assetId);
    }
}
