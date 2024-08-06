namespace HumanManagement.Models.Dto
{
    public class RewardAndDisciplineReportDto
    {
        public List<MonthlyReportDto> Reports { get; set; } = new List<MonthlyReportDto>();

        public class MonthlyReportDto
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public int TotalDisciplines { get; set; }
            public int TotalRewards { get; set; }
            //public List<Discipline> Disciplines { get; set; } = new List<Discipline>();
            //public List<Reward> Rewards { get; set; } = new List<Reward>();
        }
    }
}
