using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly DBContext _context;

        public AssetRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Asset> CreateAssetAsync(Asset asset)
        {
            await _context.Assets.AddAsync(asset);
            await SaveAsync();
            return asset;
        }

        public async Task<Asset> DeleteAssetAsync(int assetId)
        {
            var asset = await GetAssetByIdAsync(assetId);
            if (asset == null) return null;

            _context.Assets.Remove(asset);
            await SaveAsync();
            return asset;
        }

        public async Task<Asset> GetAssetByIdAsync(int assetId)
        {
            return await _context.Assets.FindAsync(assetId);
        }

        public async Task<List<Asset>> GetAssetsByActiveAsync(bool active)
        {
            return await _context.Assets
                .Where(a => a.Active == active)
                .ToListAsync();
        }

        public async Task<List<Asset>> GetAssetsAsync()
        {
            return await _context.Assets.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Asset> UpdateAssetAsync(Asset asset)
        {
            var assetUpdate = await GetAssetByIdAsync(asset.Id);
            if (assetUpdate == null) return null;

            _context.Entry(assetUpdate).CurrentValues.SetValues(asset);
            await SaveAsync();
            return asset;
        }

    }
}
