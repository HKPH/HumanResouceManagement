using HumanManagement.Models;
using HumanManagement.Models.Dto;
namespace HumanManagement.Data.Repository.Interface
{
    public interface IBankRepository
    {
        List<Bank> GetBanks();
        List<Bank> GetBanksByActive(bool active);
        Bank GetBankById(int bankId);
        bool HasBank(int bankId);
        bool CreateBank(Bank bank);
        bool Save();
        Bank checkBankByName(Bank bank);
        bool UpdateBank(Bank bank);
        bool DeleteBank(int bankId);
    }
}
