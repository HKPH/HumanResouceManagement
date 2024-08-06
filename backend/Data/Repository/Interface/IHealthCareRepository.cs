using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IHealthCareRepository
    {
        Task<List<HealthCare>> GetHealthCaresAsync();
        Task<List<HealthCare>> GetHealthCaresByActiveAsync(bool active);
        Task<HealthCare> GetHealthCareByIdAsync(int healthCareId);
        Task<HealthCare> CreateHealthCareAsync(HealthCare healthCare);
        Task<HealthCare> UpdateHealthCareAsync(HealthCare healthCare);
        Task<HealthCare> DeleteHealthCareAsync(int healthCareId);
    }
}
