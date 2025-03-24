using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IAssetRepository
    {
        Task<List<Asset>> GetAssetsAsync();
        Task<Asset> GetAssetByIdAsync(int assetId);
        Task<List<Asset>> GetAssetsByActiveAsync(bool active);
        Task<bool> SaveAsync();
        Task<Asset> CreateAssetAsync(Asset asset);
        Task<Asset> UpdateAssetAsync(Asset asset);
        Task<Asset> DeleteAssetAsync(int assetId);
    }
}
