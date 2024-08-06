using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using HumanManagement.Services.Interfaces;


namespace HumanManagement.Services
{
    public class ContractReportService : IContractReportService
    {
        private readonly IContractTypeRepository _contractTypeRepository;

        public ContractReportService(IContractTypeRepository repository)
        {
            _contractTypeRepository = repository;
        }

        public async Task<List<ContractTypeReportDto>> GetContractTypesReportAsync()
        {
            var contractTypes = await _contractTypeRepository.GetContractTypesByActiveAsync(true);

            var data = contractTypes.Select(ct => new ContractTypeReportDto
            {
                ContractId = ct.Id,
                ContractName = ct.Name,
                EmployeeCount = ct.EmployeeContracts.Count(ec => ec.Active == true)
            }).ToList();

            return data;
        }
    }
}
