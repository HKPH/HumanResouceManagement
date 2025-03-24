namespace HumanManagement.Models.Dto
{
    public class EmployeeContractDto
    {
        public int Id { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? Active { get; set; } = true;

        public int? ContractTypeId { get; set; }

        public int? EmployeeId { get; set; }


    }
}
