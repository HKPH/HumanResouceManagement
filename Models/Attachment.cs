using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Attachment
{
    public int Id { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public DateTime? UploadDate { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }
}
