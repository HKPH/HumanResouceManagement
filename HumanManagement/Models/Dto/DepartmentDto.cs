namespace HumanManagement.Models.Dto
{
    public partial class DepartmentDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public bool? Active { get; set; } = true;

        public int? ParentDepartmentId { get; set; } = null;

    }
}
