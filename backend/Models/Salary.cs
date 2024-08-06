using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Salary
{
    public int Id { get; set; }

    public decimal? BaseSalary { get; set; }

    public decimal? FinalSalary { get; set; }

    public int? EmployeeId { get; set; }

    public double? SalaryFactor { get; set; }

    public DateTime? CreaterDate { get; set; }

    public int? CreaterId { get; set; }

    public string? Note { get; set; }

    public bool? Active { get; set; }

    public virtual Employee? Employee { get; set; }
}
