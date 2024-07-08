using HumanManagement.Models;

using HumanManagement.Models.Dto;
namespace HumanManagement.Data.Repository
{
    public interface IAccountRepository
    {
        ICollection<Account> GetAccounts();
        Account GetAccountById(int accountId);
        bool HasAccount(Account account);
        bool CreateAccount(Account account);
        bool UpdateAccount(Account account);
        bool DeleteAccount(Account account);
        bool Save();
        Account CheckAccountByUsernameAndPassword(AccountDto accountDto);
        ICollection<Account> GetAccountsByActive(bool active);

    }
}
