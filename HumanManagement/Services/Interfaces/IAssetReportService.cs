using HumanManagement.Models.Dto;

namespace HumanManagement.Services.Interfaces
{
    public interface IAssetReportService
    { 
        Task<List<AssetReportDto>> GetAssetReportAsync();
    }
}
