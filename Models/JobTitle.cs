using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class JobTitle
{
    public int JobTitleId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
