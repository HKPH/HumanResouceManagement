using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class EmployeeDecision
{
    public int MakerId { get; set; }

    public int ReciverId { get; set; }

    public int DecisionId { get; set; }

    public virtual Decision Decision { get; set; } = null!;

    public virtual Employee Maker { get; set; } = null!;

    public virtual Employee Reciver { get; set; } = null!;
}
