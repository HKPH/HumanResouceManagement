namespace HumanManagement.Models.Dto
{
    public class RewardDto
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? CreateDate { get; set; }=DateTime.Now;

        public int? EmployeeId { get; set; }

        public int? CreaterId { get; set; }
    }
}
