﻿using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Salary
{
    public int SalaryId { get; set; }

    public decimal? BaseSalary { get; set; }

    public decimal? FinalSalary { get; set; }

    public DateOnly? ReceivedDate { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }
}
