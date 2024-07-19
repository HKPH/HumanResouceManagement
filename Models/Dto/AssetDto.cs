namespace HumanManagement.Models.Dto
{
    public class AssetDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime? PurchaseDate { get; set; }=DateTime.Now;

        public decimal? PurchasePrice { get; set; }

        public string? Note { get; set; }

        public int? Quantity { get; set; }

        public bool? Active { get; set; } = true;
    }
}
