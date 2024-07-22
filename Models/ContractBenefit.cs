using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class ContractBenefit
{
    public int BenefitId { get; set; }


    public DateTime? CreateDate { get; set; }

    public int? CreaterId { get; set; }

    public bool? Active { get; set; }

    public int ContractTypeId { get; set; }

    public virtual Benefit Benefit { get; set; } = null!;

    public virtual ContractType ContractType { get; set; } = null!;
}
