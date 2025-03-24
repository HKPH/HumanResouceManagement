using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using HumanManagement.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanManagement.Services
{
    public class AssetReportService : IAssetReportService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IEmployeeAssetRepository _employeeAssetRepository;

        public AssetReportService(IAssetRepository assetRepository, IEmployeeAssetRepository employeeAssetRepository)
        {
            _assetRepository = assetRepository;
            _employeeAssetRepository = employeeAssetRepository;
        }

        public async Task<List<AssetReportDto>> GetAssetReportAsync()
        {

            var borrowedAssetsDict = new Dictionary<int, int>();

            var borrowedAssets = await _employeeAssetRepository.GetBorrowedAssetsAsync();
            foreach (var borrowedAsset in borrowedAssets)
            {
                int assetId = borrowedAsset.Id;

                if (borrowedAssetsDict.ContainsKey(assetId))
                {
                    borrowedAssetsDict[assetId] += borrowedAsset.Quantity ?? 0;
                }
                else
                {
                    borrowedAssetsDict[assetId] = 1;
                }
            }

            var assetReports = new List<AssetReportDto>();
            var totalAssets = await _assetRepository.GetAssetsAsync();

            foreach (var asset in totalAssets)
            {
                var assetReport = new AssetReportDto
                {
                    Id = asset.Id,
                    Name = asset.Name,
                    TotalQuantity = asset.Quantity ?? 0,
                    BorrowedQuantity = borrowedAssetsDict.ContainsKey(asset.Id) ? borrowedAssetsDict[asset.Id] : 0
                };
                assetReports.Add(assetReport);
            }

            return assetReports;
        }
    }
}
