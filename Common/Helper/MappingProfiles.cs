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

            CreateMap<ContractAllowance, ContractAllowanceDto>();
            CreateMap<ContractAllowanceDto, ContractAllowance>();

            CreateMap<ContractBenefit, ContractBenefitDto>();
            CreateMap<ContractBenefitDto, ContractBenefit>();

            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();

            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();

            CreateMap<EmployeeProcess, EmployeeProcessDto>();
            CreateMap<EmployeeProcessDto, EmployeeProcess>();

            CreateMap<EmployeeFamily, EmployeeFamilyDto>();
            CreateMap<EmployeeFamilyDto, EmployeeFamily>();

            CreateMap<Salary, SalaryDto>();
            CreateMap<SalaryDto, Salary>();

            CreateMap<EmployeeAsset, EmployeeAssetDto>();
            CreateMap<EmployeeAssetDto, EmployeeAsset>();

            CreateMap<Discipline, DisciplineDto>();
            CreateMap<DisciplineDto, Discipline>();

            CreateMap<Reward, RewardDto>();
            CreateMap<RewardDto, Reward>();

        }
    }
}
