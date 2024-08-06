namespace HumanManagement.Models.Dto
{
    public class EmployeeAssetDto
    {
        public string? Note { get; set; }

        public DateTime? ReceivedDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int AssetId { get; set; }

        public int? Number { get; set; }

        public int EmployeeId { get; set; }

        public int? CreaterId { get; set; }

        public DateTime? CreateDate { get; set; }=DateTime.Now;

        public bool? Active { get; set; }
    }
}
