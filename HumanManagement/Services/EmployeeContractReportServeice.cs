using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models.Dto;
using HumanManagement.Services.Interfaces;

namespace HumanManagement.Services
{
    public class EmployeeContractReportService : IEmployeeContractReportService

    {
        private readonly IEmployeeContractRepository _employeeContractRepository;

        public EmployeeContractReportService(IEmployeeContractRepository employeeContractRepository)
        {
            _employeeContractRepository = employeeContractRepository;
        }

        public async Task<EmployeeContractReportDto> GetEmployeeContractReportAsync(DateTime startDate, DateTime endDate)
        {
            var newEmployees = await _employeeContractRepository.GetNewEmployeesAsync(startDate, endDate);
            var terminatedEmployees = await _employeeContractRepository.GetTerminatedEmployeesAsync(startDate, endDate);
            var totalEmployees = await _employeeContractRepository.GetEmployeesByDateAsync(startDate);

            return new EmployeeContractReportDto
            {
                NewEmployeesCount = newEmployees.Count,
                TerminatedEmployeesCount = terminatedEmployees.Count,
                TotalEmployeeCount = totalEmployees.Count,
                NewEmployees = newEmployees,
                TerminatedEmployees = terminatedEmployees
            };
        }
    }
}
