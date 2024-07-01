using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? Name { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Dob { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public int? HealthCareId { get; set; }

    public int? BankBranchId { get; set; }

    public int? DepartmentId { get; set; }

    public int? JobTitleId { get; set; }

    public int? AddressId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual Address? Address { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual BankBranch? BankBranch { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();

    public virtual ICollection<EmployeeAllowance> EmployeeAllowances { get; set; } = new List<EmployeeAllowance>();

    public virtual ICollection<EmployeeAsset> EmployeeAssets { get; set; } = new List<EmployeeAsset>();

    public virtual ICollection<EmployeeContract> EmployeeContracts { get; set; } = new List<EmployeeContract>();

    public virtual ICollection<EmployeeDecision> EmployeeDecisionMakers { get; set; } = new List<EmployeeDecision>();

    public virtual ICollection<EmployeeDecision> EmployeeDecisionRecivers { get; set; } = new List<EmployeeDecision>();

    public virtual ICollection<EmployeeFamily> EmployeeFamilies { get; set; } = new List<EmployeeFamily>();

    public virtual ICollection<EmployeeProcess> EmployeeProcesses { get; set; } = new List<EmployeeProcess>();

    public virtual HealthCare? HealthCare { get; set; }

    public virtual JobTitle? JobTitle { get; set; }

    public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

    public virtual ICollection<Resignation> Resignations { get; set; } = new List<Resignation>();

    public virtual ICollection<Reward> Rewards { get; set; } = new List<Reward>();

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

    public virtual ICollection<Benefit> Benefits { get; set; } = new List<Benefit>();
}
