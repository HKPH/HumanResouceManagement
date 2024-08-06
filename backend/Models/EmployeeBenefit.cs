using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class EmployeeBenefit
{
    public string? Note { get; set; }

    public DateTime? ReceivedDate { get; set; }

    public int BenefitId { get; set; }

    public int EmployeeId { get; set; }

    public int? CreaterId { get; set; }

    public DateTime? CreateDate { get; set; }

    public bool? Active { get; set; }

    public virtual Benefit Benefit { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
