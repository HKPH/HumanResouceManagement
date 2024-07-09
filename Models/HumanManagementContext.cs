using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Models;

public partial class HumanManagementContext : DbContext
{
    public HumanManagementContext()
    {
    }

    public HumanManagementContext(DbContextOptions<HumanManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Allowance> Allowances { get; set; }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<BankBranch> BankBranches { get; set; }

    public virtual DbSet<Benefit> Benefits { get; set; }

    public virtual DbSet<ContractType> ContractTypes { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DepartmentJobTitle> DepartmentJobTitles { get; set; }

    public virtual DbSet<Discipline> Disciplines { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeAsset> EmployeeAssets { get; set; }

    public virtual DbSet<EmployeeContract> EmployeeContracts { get; set; }

    public virtual DbSet<EmployeeCv> EmployeeCvs { get; set; }

    public virtual DbSet<EmployeeFamily> EmployeeFamilies { get; set; }

    public virtual DbSet<EmployeeProcess> EmployeeProcesses { get; set; }

    public virtual DbSet<HealthCare> HealthCares { get; set; }

    public virtual DbSet<JobTitle> JobTitles { get; set; }

    public virtual DbSet<Resignation> Resignations { get; set; }

    public virtual DbSet<Reward> Rewards { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<SalaryAllowance> SalaryAllowances { get; set; }

    public virtual DbSet<SalaryBenefit> SalaryBenefits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=KHOIPH\\PHK;Initial Catalog=HumanManagement;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__349DA5A611A9F248");

            entity.ToTable("Account");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Employee).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Account__Employe__6B24EA82");
        });

        modelBuilder.Entity<Allowance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Allowanc__7B12D1A182532F09");

            entity.ToTable("Allowance");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Asset__434923528AE47358");

            entity.ToTable("Asset");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attachme__442C64BE83211A44");

            entity.ToTable("Attachment");

            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.FilePath).HasMaxLength(255);

            entity.HasOne(d => d.Employee).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Attachmen__Emplo__6E01572D");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bank__AA08CB13BC0613B0");

            entity.ToTable("Bank");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Web).HasMaxLength(100);
        });

        modelBuilder.Entity<BankBranch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BankBran__135DBAB545BDCC58");

            entity.ToTable("BankBranch");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);

            entity.HasOne(d => d.Bank).WithMany(p => p.BankBranches)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK__BankBranc__BankI__2C3393D0");
        });

        modelBuilder.Entity<Benefit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Benefit__5754C6DA9CA4DE24");

            entity.ToTable("Benefit");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ContractType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contract__68A61565FC0C918F");

            entity.ToTable("ContractType");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__B2079BED86A84897");

            entity.ToTable("Department");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<DepartmentJobTitle>(entity =>
        {
            entity.HasKey(e => new { e.DepartmentId, e.JobTitleId }).HasName("PK__Departme__92F3201724377229");

            entity.ToTable("Department_JobTitle");

            entity.HasOne(d => d.Department).WithMany(p => p.DepartmentJobTitles)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Departmen__depar__5CA1C101");

            entity.HasOne(d => d.JobTitle).WithMany(p => p.DepartmentJobTitles)
                .HasForeignKey(d => d.JobTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Departmen__jobTi__5D95E53A");
        });

        modelBuilder.Entity<Discipline>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Discipli__290937705FD007D3");

            entity.ToTable("Discipline");

            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.Employee).WithMany(p => p.Disciplines)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Disciplin__Emplo__619B8048");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__7AD04F111C062C22");

            entity.ToTable("Employee");

            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);

            entity.HasOne(d => d.BankBranch).WithMany(p => p.Employees)
                .HasForeignKey(d => d.BankBranchId)
                .HasConstraintName("FK__Employee__BankBr__3B75D760");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employee__Depart__3C69FB99");

            entity.HasOne(d => d.HealthCare).WithMany(p => p.Employees)
                .HasForeignKey(d => d.HealthCareId)
                .HasConstraintName("FK__Employee__Health__3A81B327");

            entity.HasOne(d => d.JobTitle).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobTitleId)
                .HasConstraintName("FK__Employee__JobTit__3D5E1FD2");
        });

        modelBuilder.Entity<EmployeeAsset>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.AssetId }).HasName("PK__Employee__4EE4DD24EA383F63");

            entity.ToTable("Employee_Asset");

            entity.Property(e => e.Note).HasMaxLength(255);

            entity.HasOne(d => d.Asset).WithMany(p => p.EmployeeAssets)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee___Asset__5DCAEF64");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeAssets)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee___Emplo__5EBF139D");
        });

        modelBuilder.Entity<EmployeeContract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__E17E04F6C9193CBA");

            entity.ToTable("EmployeeContract");

            entity.HasOne(d => d.ContractType).WithMany(p => p.EmployeeContracts)
                .HasForeignKey(d => d.ContractTypeId)
                .HasConstraintName("FK__EmployeeC__Contr__412EB0B6");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeContracts)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__EmployeeC__Emplo__4222D4EF");
        });

        modelBuilder.Entity<EmployeeCv>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Address__091C2AFB0EE1145E");

            entity.ToTable("EmployeeCV");

            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.Province).HasMaxLength(50);
            entity.Property(e => e.Ward).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeCvs)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Address_Employee");
        });

        modelBuilder.Entity<EmployeeFamily>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__19368B1BC5A1E352");

            entity.ToTable("EmployeeFamily");

            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Note).HasMaxLength(255);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeFamilies)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_EmployeeFamily_Employee");
        });

        modelBuilder.Entity<EmployeeProcess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__C393BC8625B6F0F0");

            entity.ToTable("EmployeeProcess");

            entity.Property(e => e.WorkingProcessOutside).HasMaxLength(255);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeProcesses)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__EmployeeP__Emplo__5070F446");
        });

        modelBuilder.Entity<HealthCare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HealthCa__F7AD66FD06DCB0EB");

            entity.ToTable("HealthCare");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
        });

        modelBuilder.Entity<JobTitle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobTitle__35382FE964263AB0");

            entity.ToTable("JobTitle");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Resignation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Resignat__CD4E6DB55AB1B1A8");

            entity.ToTable("Resignation");

            entity.Property(e => e.Reason).HasMaxLength(255);

            entity.HasOne(d => d.Employee).WithMany(p => p.Resignations)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Resignati__Emplo__534D60F1");
        });

        modelBuilder.Entity<Reward>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reward__825015B9D2507171");

            entity.ToTable("Reward");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.Employee).WithMany(p => p.Rewards)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Reward__Employee__5629CD9C");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Salary__4BE20457FD15762D");

            entity.ToTable("Salary");

            entity.Property(e => e.BaseSalary).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.FinalSalary).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Note).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Salary__Employee__59063A47");
        });

        modelBuilder.Entity<SalaryAllowance>(entity =>
        {
            entity.HasKey(e => new { e.SalaryId, e.AllowanceId }).HasName("PK__Salary_A__1B12D0903AC06272");

            entity.ToTable("Salary_Allowance");

            entity.HasOne(d => d.Allowance).WithMany(p => p.SalaryAllowances)
                .HasForeignKey(d => d.AllowanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Salary_Allowance_Allowance");

            entity.HasOne(d => d.Salary).WithMany(p => p.SalaryAllowances)
                .HasForeignKey(d => d.SalaryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Salary_Allowance_Salary");
        });

        modelBuilder.Entity<SalaryBenefit>(entity =>
        {
            entity.HasKey(e => new { e.BalaryId, e.BenefitId }).HasName("PK__Salary_B__666E6055862FC369");

            entity.ToTable("Salary_Benefit");

            entity.HasOne(d => d.Balary).WithMany(p => p.SalaryBenefits)
                .HasForeignKey(d => d.BalaryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Salary_Be__salar__3B40CD36");

            entity.HasOne(d => d.Benefit).WithMany(p => p.SalaryBenefits)
                .HasForeignKey(d => d.BenefitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Salary_Be__benef__3C34F16F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
