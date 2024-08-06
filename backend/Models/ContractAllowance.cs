namespace HumanManagement.Models;

public partial class ContractAllowance
{

    public DateTime? CreateDate { get; set; }

    public int AllowanceId { get; set; }

    public int? CreaterId { get; set; }

    public bool? Active { get; set; }

    public int ContractTypeId { get; set; }

    public virtual Allowance Allowance { get; set; } = null!;

    public virtual ContractType ContractType { get; set; } = null!;
}
