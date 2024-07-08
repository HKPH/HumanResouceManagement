using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class SalaryBenefit
{
    public int BalaryId { get; set; }

    public int BenefitId { get; set; }

    public bool Received { get; set; }

    public DateOnly CreateDate { get; set; }

    public int? CreaterId { get; set; }

    public bool? Active { get; set; }

    public virtual Salary Balary { get; set; } = null!;

    public virtual Benefit Benefit { get; set; } = null!;
}
