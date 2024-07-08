using HumanManagement.Models;
using HumanManagement.Models.Dto;
using BCrypt.Net;
namespace HumanManagement.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        DBContext _context;
        public AccountRepository(DBContext context)
        {
            _context = context;
        }
        public Account CheckAccountByUsernameAndPassword(AccountDto accountDto)
        {
            var account = GetAccounts().FirstOrDefault(c =>
                c.Username.Trim().ToUpper() == accountDto.Username.Trim().ToUpper()
            );

            if (account != null && BCrypt.Net.BCrypt.Verify(accountDto.Password, account.Password))
            {
                return account;
            }

            return null;
        }

        public bool CreateAccount(Account account)
        {
            _context.Add(account);
            return Save();
        }

        public bool DeleteAccount(Account account)
        {
            _context.Remove(account);
            return Save();
        }

        public Account GetAccountById(int accountId)
        {
            return _context.Accounts.Where(o=>o.Id==accountId).FirstOrDefault();
        }

        public ICollection<Account> GetAccounts()
        {
            return _context.Accounts.OrderBy(a => a.Id).ToList();
        }

        public ICollection<Account> GetAccountsByActive(bool active)
        {
            return GetAccounts().Where(acc=> acc.Active==active).ToList();
        }

        public bool HasAccount(Account account)
        {
            return _context.Accounts.Any(a => a.Id == account.Id);
        }

        public bool Save()
        {
            var check = _context.SaveChanges();
            return check > 0 ? true : false;
        }

        public bool UpdateAccount(Account account)
        {
            _context.Update(account);
            return Save();
        }
    }
}
