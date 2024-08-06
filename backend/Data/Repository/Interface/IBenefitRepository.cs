using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IBenefitRepository
    {
        Task<List<Benefit>> GetBenefitsAsync();
        Task<Benefit> GetBenefitByIdAsync(int benefitId);
        Task<List<Benefit>> GetBenefitsByActiveAsync(bool active);
        Task<bool> SaveAsync();
        Task<Benefit> CreateBenefitAsync(Benefit benefit);
        Task<Benefit> UpdateBenefitAsync(Benefit benefit);
        Task<Benefit> DeleteBenefitAsync(int benefitId);
    }
}
