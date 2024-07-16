using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class ContractType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<ContractAllowance> ContractAllowances { get; set; } = new List<ContractAllowance>();

    public virtual ICollection<ContractBenefit> ContractBenefits { get; set; } = new List<ContractBenefit>();

    public virtual ICollection<EmployeeContract> EmployeeContracts { get; set; } = new List<EmployeeContract>();
}
