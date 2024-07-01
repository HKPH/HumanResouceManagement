using HumanManagement.Models;

namespace HumanManagement.Services.Interfaces
{
    public interface IBankRepository
    {
        ICollection<Bank> GetBanks();
        Bank GetBankById(int bankId);
        bool HasBank(int bankId);

        bool CreateBank(Bank bank);
        bool Save();
    }
}
