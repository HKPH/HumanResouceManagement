using HumanManagement.Models;
using HumanManagement.Models.Dto;
namespace HumanManagement.Data.Repository.Interface
{
    public interface IBankRepository
    {
        Task<List<Bank>> GetBanksAsync();
        Task<List<Bank>> GetBanksByActiveAsync(bool active);
        Task<Bank> GetBankByIdAsync(int bankId);
        Task<bool> HasBankAsync(int bankId);
        Task<Bank> CreateBankAsync(Bank bank);
        Task<bool> SaveAsync();
        Task<Bank> UpdateBankAsync(Bank bank);
        Task<Bank> DeleteBankAsync(int bankId);
    }
}
