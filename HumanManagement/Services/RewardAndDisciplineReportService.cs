using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using HumanManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HumanManagement.Models.Dto.RewardAndDisciplineReportDto;

namespace HumanManagement.Services
{
    public class RewardAndDisciplineReportService : IRewardAndDisciplineReportService
    {
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IRewardRepository _rewardRepository;

        public RewardAndDisciplineReportService(IDisciplineRepository disciplineRepository, IRewardRepository rewardRepository)
        {
            _disciplineRepository = disciplineRepository;
            _rewardRepository = rewardRepository;
        }

        public async Task<RewardAndDisciplineReportDto> GetReportByMonth(DateTime startDate, DateTime endDate)
        {
            var disciplines = await _disciplineRepository.GetDisciplinesByDateRangeAsync(startDate, endDate);
            var rewards = await _rewardRepository.GetRewardsByDateRangeAsync(startDate, endDate);
            var months = new HashSet<(int Year, int Month)>();

            for (var date = startDate; date <= endDate; date = date.AddMonths(1))
            {
                months.Add((date.Year, date.Month));
            }

            var reportList = new List<MonthlyReportDto>();

            foreach (var (year, month) in months)
            {
                var monthlyDisciplines = disciplines
                    .Where(d => d.CreateDate.Value.Year == year && d.CreateDate.Value.Month == month)
                    .ToList();

                var monthlyRewards = rewards
                    .Where(r => r.CreateDate.Value.Year == year && r.CreateDate.Value.Month == month)
                    .ToList();

                var monthlyReport = new MonthlyReportDto
                {
                    Year = year,
                    Month = month,
                    TotalDisciplines = monthlyDisciplines.Count,
                    TotalRewards = monthlyRewards.Count,

                    //Disciplines = monthlyDisciplines,
                    //Rewards = monthlyRewards
                };

                reportList.Add(monthlyReport);
            }

            return new RewardAndDisciplineReportDto
            {
                Reports = reportList
            };
        }

    }
}
