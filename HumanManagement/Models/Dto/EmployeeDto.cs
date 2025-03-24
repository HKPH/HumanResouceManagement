using System;
using System.Collections.Generic;

namespace HumanManagement.Models.Dto;

public partial class EmployeeDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Gender { get; set; }

    public DateTime Dob { get; set; }
    public DateTime CreateDate { get; set; }= DateTime.Now;

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public int? HealthCareId { get; set; }

    public int? BankBranchId { get; set; }

    public int? DepartmentId { get; set; }

    public int? JobTitleId { get; set; }

    public bool? Active { get; set; }=true;

}
