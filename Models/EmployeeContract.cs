using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class EmployeeContract
{
    public int EmployeeContractId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool? Active { get; set; }

    public int? ContractTypeId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual ContractType? ContractType { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();
}
