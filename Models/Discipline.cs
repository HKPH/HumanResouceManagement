﻿using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Discipline
{
    public int DisciplineId { get; set; }

    public string? Description { get; set; }

    public bool? Applied { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }
}
