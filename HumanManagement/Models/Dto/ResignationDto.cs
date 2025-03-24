namespace HumanManagement.Models.Dto
{
    public partial class ResignationDto
    {
        public int Id { get; set; }

        public string? Reason { get; set; }

        public DateTime? EffectiveDay { get; set; }

        public int? EmployeeId { get; set; }

        public DateTime? CreateDate { get; set; }= DateTime.Now;
        public Boolean Accepted { get; set; } =false;

    }

}
