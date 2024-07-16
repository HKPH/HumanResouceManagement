using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IAssetRepository
    {
        List<Asset> GetAssets();
        Asset GetAssetById(int assetId);
        List<Asset> GetAssetsByActive(bool active);
        bool Save();
        bool CreateAsset(Asset asset);
        bool UpdateAsset(Asset asset);
        bool DeleteAsset(int assetId);
    }
}
