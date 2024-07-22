using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class DepartmentJobTitleDto
{
    public int DepartmentId { get; set; }
    public DateTime? CreateDate { get; set; } = DateTime.Now;


    public int JobTitleId { get; set; }

    public bool Active { get; set; } = true;

    public virtual Department Department { get; set; } = null!;

    public virtual JobTitle JobTitle { get; set; } = null!;
}
