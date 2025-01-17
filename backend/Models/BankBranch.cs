﻿namespace HumanManagement.Models;

public partial class BankBranch
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public int? BankId { get; set; }

    public bool? Active { get; set; }

    public virtual Bank? Bank { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
