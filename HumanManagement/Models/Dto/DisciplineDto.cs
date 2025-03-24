namespace HumanManagement.Models.Dto
{
    public class DisciplineDto
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public bool? Applied { get; set; }

        public int? EmployeeId { get; set; }

        public int? CreaterId { get; set; }

        public DateTime? CreateDate { get; set; }= DateTime.Now;
    }
}
