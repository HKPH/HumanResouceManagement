using HumanManagement.Models;
using HumanManagement.Models.Dto;

namespace HumanManagement.Services.Interfaces
{
    public interface IContractReportService
    {
        Task<List<ContractTypeReportDto>> GetContractTypesReportAsync();
    }
}
