﻿using HumanManagement.Models;
using HumanManagement.Models.Dto;
using System.Diagnostics.Eventing.Reader;

namespace HumanManagement.Data.Repository
{
    public interface IBankBranchRepository
    {
        ICollection<BankBranch> GetBankBranches();
        BankBranch GetBankBranchById(int bankBranchId);
        bool HasBankBranch(int bankBranchId);
        bool CreateBankBranch(BankBranch bankBranch);
        bool UpdateBankBranch(BankBranch bankBranch);
        BankBranch checkBankBranchByName(BankBranchDto bankBranch);
        bool Save();
        bool DeleteBankBranch(BankBranch bankBranch);
        bool DeleteBankBranches(List<BankBranch> bankBranches); 
        List<BankBranch> GetAllBankBranchesByBankId(int bankId);
        ICollection<BankBranch> GetBankBranchesByActive(bool active);
        

    }
}