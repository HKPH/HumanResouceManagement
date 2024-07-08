using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Allowance
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Amount { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<SalaryAllowance> SalaryAllowances { get; set; } = new List<SalaryAllowance>();
}
