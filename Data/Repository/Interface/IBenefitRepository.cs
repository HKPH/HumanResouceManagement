using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IBenefitRepository
    {
        List<Benefit> GetBenefits();
        Benefit GetBenefitById(int benefitId);
        List<Benefit> GetBenefitsByActive(bool active);
        bool Save();
        bool CreateBenefit(Benefit benefit);
        bool UpdateBenefit(Benefit benefit);
        bool DeleteBenefit(int benefitId);
    }
}
