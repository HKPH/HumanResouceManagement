namespace HumanManagement.Models.Dto
{
    public class AssetReportDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalQuantity { get; set; }
        public int BorrowedQuantity { get; set; }
    }
}
