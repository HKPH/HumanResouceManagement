using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class HealthCare
{
    public int HealthCareId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
