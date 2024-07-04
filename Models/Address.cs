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

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }
}
