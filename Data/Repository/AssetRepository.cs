using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public class AssetRepository:IAssetRepository
    {
        private readonly DBContext _context;
        public AssetRepository(DBContext context)
        {
            _context = context;
        }

        public bool CreateAsset(Asset asset)
        {
            _context.Add(asset);
            return Save();
        }

        public bool DeleteAsset(int assetId)
        {
            var asset=GetAssetById(assetId);
            if(asset==null)
                return false;
            _context.Remove(asset);
            return Save();
        }

        public Asset GetAssetById(int assetId)
        {
            return _context.Assets.Where(a => a.Id == assetId).FirstOrDefault();
        }

        public List<Asset> GetAssets()
        {
            return _context.Assets.ToList();
        }

        public List<Asset> GetAssetsByActive(bool active)
        {
            return _context.Assets.Where(a => a.Active == active).ToList();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateAsset(Asset asset)
        {
            
            var assetUpdate=GetAssetById(asset.Id);
            if(assetUpdate==null)
                return false;
            _context.Entry(assetUpdate).CurrentValues.SetValues(asset);
            return Save();
        }
    }
}
