using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class EmployeeFamily
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? Dob { get; set; }

    public string? Note { get; set; }

    public int? EmployeeId { get; set; }

    public bool? DependentDeduction { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public int? Gender { get; set; }

    public virtual Employee? Employee { get; set; }
}
