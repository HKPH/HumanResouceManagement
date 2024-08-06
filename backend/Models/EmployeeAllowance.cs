using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class EmployeeAllowance
{
    public string? Note { get; set; }

    public DateTime? ReceivedDate { get; set; }

    public int AllowanceId { get; set; }

    public int EmployeeId { get; set; }

    public int? CreaterId { get; set; }

    public DateTime? CreateDate { get; set; }

    public bool? Active { get; set; }

    public virtual Allowance Allowance { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
