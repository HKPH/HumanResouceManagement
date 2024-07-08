using System;
using System.Collections.Generic;

namespace HumanManagement.Models.Dto;

public partial class BankBranchDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public int? BankId { get; set; }

    public bool? Active { get; set; }


}
