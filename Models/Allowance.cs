using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Allowance
{
    public int AllowanceId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Amount { get; set; }

    public virtual ICollection<EmployeeAllowance> EmployeeAllowances { get; set; } = new List<EmployeeAllowance>();
}
