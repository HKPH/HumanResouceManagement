namespace HumanManagement.Models.Dto
{
    public class EmployeeContractReportDto
    {
        public int NewEmployeesCount { get; set; }
        public int TerminatedEmployeesCount { get; set; }
        public int TotalEmployeeCount { get; set; }
        public List<Employee> NewEmployees { get; set; } = new List<Employee>();
        public List<Employee> TerminatedEmployees { get; set; } = new List<Employee>();


    }

}
