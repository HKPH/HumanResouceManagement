namespace HumanManagement.Models;

public partial class DepartmentJobTitle
{
    public int DepartmentId { get; set; }
    public DateTime? CreateDate { get; set; }

    public int JobTitleId { get; set; }

    public bool Active { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual JobTitle JobTitle { get; set; } = null!;
}
