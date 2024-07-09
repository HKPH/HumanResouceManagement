using HumanManagement.Models;
using HumanManagement.Models.Dto;
namespace HumanManagement.Data.Repository
{
    public interface IHealthCareRepository
    {
        List<HealthCare> GetHealthCares();
        List<HealthCare> GetHealthCareByActive(bool active);
        HealthCare GetHealthCareById(int healthCareId);
        bool CreateHealthCare(HealthCare healthCare);
        bool UpdateHealthCare(HealthCare healthCare);
        bool DeleteHealthCare(HealthCare healthCare);
        bool Save();

    }
}
