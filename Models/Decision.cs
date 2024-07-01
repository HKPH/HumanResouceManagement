using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Decision
{
    public int DecisionId { get; set; }

    public DateOnly? DecisionDate { get; set; }

    public string? Detail { get; set; }

    public DateOnly? EffectiveDate { get; set; }

    public virtual ICollection<EmployeeDecision> EmployeeDecisions { get; set; } = new List<EmployeeDecision>();

    public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();
}
