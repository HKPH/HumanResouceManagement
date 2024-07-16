using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Reward
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public decimal? Amount { get; set; }

    public bool? Received { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? EmployeeId { get; set; }

    public int? CreaterId { get; set; }

    public virtual Employee? Employee { get; set; }
}
