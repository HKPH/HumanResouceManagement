using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class EmployeeFamily
{
    public int EmployeeFamilyId { get; set; }

    public string? Name { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Note { get; set; }

    public int? EmployeeId { get; set; }

    public int? EmployeeProcessId { get; set; }

    public bool? DependentDeduction { get; set; }

    public DateOnly? EffectiveDate { get; set; }

    public virtual EmployeeProcess? EmployeeProcess { get; set; }
}
