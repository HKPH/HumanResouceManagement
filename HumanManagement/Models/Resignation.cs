using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Resignation
{
    public int Id { get; set; }

    public string? Reason { get; set; }

    public DateTime? EffectiveDay { get; set; }

    public int? EmployeeId { get; set; }
    public DateTime? CreateDate { get; set; }
    public Boolean Accepted { get; set; }

    public virtual Employee? Employee { get; set; }

}
