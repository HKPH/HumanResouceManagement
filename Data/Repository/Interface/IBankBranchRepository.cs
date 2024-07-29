using HumanManagement.Models;
using HumanManagement.Models.Dto;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IBankBranchRepository
    {
        Task<List<BankBranch>> GetBankBranchesAsync();
        Task<BankBranch> GetBankBranchByIdAsync(int bankBranchId);
        Task<bool> HasBankBranchAsync(int bankBranchId);
        Task<BankBranch> CreateBankBranchAsync(BankBranch bankBranch);
        Task<BankBranch> UpdateBankBranchAsync(BankBranch bankBranch);
        Task<BankBranch> CheckBankBranchByNameAsync(BankBranchDto bankBranch);
        Task<bool> SaveAsync();
        Task<BankBranch> DeleteBankBranchAsync(int bankBranchId);
        Task<List<BankBranch>> GetAllBankBranchesByBankIdAsync(int bankId);
        Task<List<BankBranch>> GetBankBranchesByActiveAsync(bool active);
    }
}
