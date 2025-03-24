namespace HumanManagement.Models;

public partial class ContractBenefitDto
{
    public int BenefitId { get; set; }

    public DateTime? CreateDate { get; set; }=DateTime.Now;

    public int? CreaterId { get; set; }

    public bool? Active { get; set; } = true;

    public int ContractTypeId { get; set; }

}
