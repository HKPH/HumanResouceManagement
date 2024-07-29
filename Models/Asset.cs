namespace HumanManagement.Models;

public partial class Asset
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? PurchaseDate { get; set; }
    
    public decimal? PurchasePrice { get; set; }

    public string? Note { get; set; }

    public int? Quantity { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<EmployeeAsset> EmployeeAssets { get; set; } = new List<EmployeeAsset>();
}
