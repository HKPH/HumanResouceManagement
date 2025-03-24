namespace HumanManagement.Models.Dto
{
    public class EmployeeAllowanceDto
    {
        public string? Note { get; set; }

        public DateTime? ReceivedDate { get; set; }

        public int AllowanceId { get; set; }

        public int EmployeeId { get; set; }

        public int? CreaterId { get; set; }

        public DateTime? CreateDate { get; set; }= DateTime.Now;

        public bool? Active { get; set; } = true;
    }
}
