using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class EmployeeProcess
{
    public int Id { get; set; }

    public string? WorkingProcessOutside { get; set; }

    public int? EmployeeId { get; set; }

    public bool? Active { get; set; }

    public virtual Employee? Employee { get; set; }
}
