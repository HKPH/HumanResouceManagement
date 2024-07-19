namespace HumanManagement.Models.Dto
{
    public class AllowanceDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Amount { get; set; }

        public bool? Active { get; set; }=true;
    }
}
