﻿namespace HumanManagement.Models;

public partial class Account
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public string? Email { get; set; }

    public DateTime? CreateDate { get; set; } = DateTime.Now;

    public int? EmployeeId { get; set; }

    public bool? Active { get; set; }

    public virtual Employee? Employee { get; set; }
}
