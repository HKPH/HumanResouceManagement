using AutoMapper;
using HumanManagement.Models;
using HumanManagement.Models.Dto;

namespace HumanManagement.Common.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AccountDto, Account>();
            CreateMap<Account, AccountDto>();

            CreateMap<Allowance, AllowanceDto>();
            CreateMap<AllowanceDto, Allowance>();

            CreateMap<Asset, AssetDto>();
            CreateMap<AssetDto, Asset>();


            CreateMap<Bank, BankDto>();
            CreateMap<BankDto, Bank>();
            CreateMap<BankBranch, BankBranchDto>();
            CreateMap<BankBranchDto, BankBranch>();

            CreateMap<HealthCare, HealthCareDto>();
            CreateMap<HealthCareDto, HealthCare>();
            CreateMap<EmployeeCv, EmployeeCvDto>();
            CreateMap<EmployeeCvDto, EmployeeCv>();
            CreateMap<JobTitleDto, JobTitle>();
            CreateMap<JobTitle, JobTitleDto>();
            CreateMap<ContractType, ContractTypeDto>();
            CreateMap<ContractTypeDto, ContractType>();
            CreateMap<EmployeeContractDto, EmployeeContract>();
            CreateMap<EmployeeContract, EmployeeContractDto>();

            CreateMap<BenefitDto, Benefit>();
            CreateMap<Benefit, BenefitDto>();









        }
    }
}
