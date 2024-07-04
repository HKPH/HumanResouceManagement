using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class DepartmentJobTitle
{
    public int DepartmentId { get; set; }

    public int JobTitleId { get; set; }

    public bool Active { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual JobTitle JobTitle { get; set; } = null!;
}
