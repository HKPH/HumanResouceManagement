namespace HumanManagement.Models.Dto
{
    public class BenefitDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Amount { get; set; } = 0;

        public bool? Active { get; set; } = true;
    }
}
