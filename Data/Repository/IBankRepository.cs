using HumanManagement.Models;
using HumanManagement.Models.Dto;
namespace HumanManagement.Data.Repository
{
    public interface IBankRepository
    {
        ICollection<Bank> GetBanks();
        Bank GetBankById(int bankId);
        bool HasBank(int bankId);

        bool CreateBank(Bank bank);
        bool Save();
        Bank checkBankByName(BankDto bank);
        bool UpdateBank(Bank bank);
        bool DeleteBank(Bank bank);
    }
}
