using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Discipline
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public bool? Applied { get; set; }

    public int? EmployeeId { get; set; }

    public int? CreaterId { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Employee? Employee { get; set; }
}
