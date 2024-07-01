using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class EmployeeAllowance
{
    public bool? Received { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int EmployeeId { get; set; }

    public int AllowanceId { get; set; }

    public virtual Allowance Allowance { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
