namespace HumanManagement.Models.Dto
{
    public class EmployeeCvDto
    {
        public int Id { get; set; }

        public string? Country { get; set; }

        public string? Province { get; set; }

        public string? District { get; set; }

        public string? Ward { get; set; }

        public int? EmployeeId { get; set; }

        public bool? Active { get; set; }
    }
}
