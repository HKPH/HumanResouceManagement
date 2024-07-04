using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class SalaryAllowance
{
    public bool Received { get; set; }

    public DateOnly CreateDate { get; set; }

    public int SalaryId { get; set; }

    public int AllowanceId { get; set; }

    public virtual Allowance Allowance { get; set; } = null!;

    public virtual Salary Salary { get; set; } = null!;
}
