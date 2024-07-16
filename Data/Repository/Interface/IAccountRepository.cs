using HumanManagement.Models;

using HumanManagement.Models.Dto;
namespace HumanManagement.Data.Repository.Interface
{
    public interface IAccountRepository
    {
        List<Account> GetAccounts();
        Account GetAccountById(int accountId);
        bool HasAccount(Account account);
        bool CreateAccount(Account account);
        bool UpdateAccount(Account account);
        bool DeleteAccount(int accountId);
        bool Save();
        Account CheckAccountByUsernameAndPassword(AccountDto accountDto);
        List<Account> GetAccountsByActive(bool active);

    }
}
