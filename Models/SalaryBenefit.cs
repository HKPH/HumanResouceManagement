using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class SalaryBenefit
{
    public int SalaryId { get; set; }

    public int BenefitId { get; set; }

    public bool Received { get; set; }

    public DateOnly CreateDate { get; set; }

    public virtual Benefit Benefit { get; set; } = null!;

    public virtual Salary Salary { get; set; } = null!;
}
