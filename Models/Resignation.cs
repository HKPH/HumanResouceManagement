using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Resignation
{
    public int ResignationId { get; set; }

    public string? Reason { get; set; }

    public DateOnly? EffectiveDay { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }
}
