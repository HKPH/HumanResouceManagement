using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class EmployeeAsset
{
    public string? Note { get; set; }

    public DateOnly? ReceivedDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public int AssetId { get; set; }

    public int? Number { get; set; }

    public int EmployeeId { get; set; }

    public int? CreaterId { get; set; }

    public DateOnly? CreateDate { get; set; }

    public bool? Active { get; set; }

    public virtual Asset Asset { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
