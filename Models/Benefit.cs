using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Benefit
{
    public int BenefitId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Amount { get; set; }

    public virtual ICollection<SalaryBenefit> SalaryBenefits { get; set; } = new List<SalaryBenefit>();
}
