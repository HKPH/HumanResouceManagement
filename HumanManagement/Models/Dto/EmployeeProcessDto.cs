namespace HumanManagement.Models.Dto
{
    public class EmployeeProcessDto
    {
        public int Id { get; set; }

        public string? WorkingProcessOutside { get; set; }

        public int? EmployeeId { get; set; }

        public bool? Active { get; set; } = true;
    }
}
