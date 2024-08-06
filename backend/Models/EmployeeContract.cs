using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class EmployeeContract
{
    public int Id { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? Active { get; set; }

    public int? ContractTypeId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual ContractType? ContractType { get; set; }

    public virtual Employee? Employee { get; set; }
}
