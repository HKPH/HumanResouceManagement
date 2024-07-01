using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Attachment
{
    public int AttachmentId { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public DateOnly? UploadDate { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }
}
