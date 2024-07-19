using HumanManagement.Models;
using HumanManagement.Models.Dto;
using BCrypt.Net;
using HumanManagement.Data.Repository.Interface;
namespace HumanManagement.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DBContext _context;
        public AccountRepository(DBContext context)
        {
            _context = context;
        }
        public Account CheckAccountByUsernameAndPassword(AccountDto accountDto)
        {
            var account = GetAccounts().FirstOrDefault(c =>
                c.Username.Trim() == accountDto.Username.Trim()&& c.Password.Trim() == accountDto.Password.Trim()
            );

            //if (account != null && BCrypt.Net.BCrypt.Verify(accountDto.Password, account.Password))
            //{
            //    return account;
            //}

            return account;
        }

        public bool CreateAccount(Account account)
        {
            _context.Add(account);
            return Save();
        }

        public bool DeleteAccount(int accountId)
        {
            var account=GetAccountById(accountId);
            _context.Remove(account);
            return Save();
        }

        public Account GetAccountById(int accountId)
        {
            return _context.Accounts.Where(o=>o.Id==accountId).FirstOrDefault();
        }

        public List<Account> GetAccounts()
        {
            return _context.Accounts.OrderBy(a => a.Id).ToList();
        }

        public List<Account> GetAccountsByActive(bool active)
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
            var accountUpdate=GetAccountById(account.Id);
            _context.Entry(accountUpdate).CurrentValues.SetValues(account);
            return Save();
        }
    }
}
