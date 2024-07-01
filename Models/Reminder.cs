using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Reminder
{
    public int ReminderId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateOnly? EffectiveDate { get; set; }

    public int? EmployeeContractId { get; set; }

    public int? EmployeeId { get; set; }

    public int? DecisionId { get; set; }

    public virtual Decision? Decision { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual EmployeeContract? EmployeeContract { get; set; }
}
