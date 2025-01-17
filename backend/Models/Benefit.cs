﻿namespace HumanManagement.Models;

public partial class Benefit
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Amount { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<ContractBenefit> ContractBenefits { get; set; } = new List<ContractBenefit>();
}
