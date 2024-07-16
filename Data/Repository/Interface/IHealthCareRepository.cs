using HumanManagement.Models;
using HumanManagement.Models.Dto;
namespace HumanManagement.Data.Repository.Interface
{
    public interface IHealthCareRepository
    {
        List<HealthCare> GetHealthCares();
        List<HealthCare> GetHealthCaresByActive(bool active);
        HealthCare GetHealthCareById(int healthCareId);
        bool CreateHealthCare(HealthCare healthCare);
        bool UpdateHealthCare(HealthCare healthCare);
        bool DeleteHealthCare(int healthCareId);
        bool Save();

    }
}
