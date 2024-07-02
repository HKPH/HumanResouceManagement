using AutoMapper;
using HumanManagement.Models;
using HumanManagement.Models.Dto;

namespace HumanManagement.Common.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Bank, BankDto>();
            CreateMap<BankDto, Bank>();
            CreateMap<BankBranch, BankBranchDto>();
            CreateMap<BankBranchDto, BankBranch>();
        }
    }
}
