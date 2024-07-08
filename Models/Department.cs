using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Department
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<DepartmentJobTitle> DepartmentJobTitles { get; set; } = new List<DepartmentJobTitle>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
