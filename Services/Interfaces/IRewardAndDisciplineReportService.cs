using HumanManagement.Models.Dto;

namespace HumanManagement.Services.Interfaces
{
    public interface IRewardAndDisciplineReportService
    {
        Task<RewardAndDisciplineReportDto> GetReportByMonth(DateTime startDate, DateTime endDate);

    }
}
