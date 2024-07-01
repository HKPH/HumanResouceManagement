using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Asset
{
    public int AssetId { get; set; }

    public string? Name { get; set; }

    public DateOnly? PurchaseDate { get; set; }

    public decimal? PurchasePrice { get; set; }

    public string? Note { get; set; }

    public int? Quantity { get; set; }

    public virtual ICollection<EmployeeAsset> EmployeeAssets { get; set; } = new List<EmployeeAsset>();
}
