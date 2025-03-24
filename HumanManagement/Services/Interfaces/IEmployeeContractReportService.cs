using HumanManagement.Models.Dto;

namespace HumanManagement.Services.Interfaces
{
    public interface IEmployeeContractReportService
    {
        Task<EmployeeContractReportDto> GetEmployeeContractReportAsync(DateTime startDate, DateTime endDate);
    }
}
