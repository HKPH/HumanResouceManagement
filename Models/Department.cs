﻿using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
