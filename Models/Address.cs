using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string? Country { get; set; }

    public string? Province { get; set; }

    public string? District { get; set; }

    public string? Ward { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
