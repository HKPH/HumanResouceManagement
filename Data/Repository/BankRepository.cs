using HumanManagement.Data;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;

using HumanManagement.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly DBContext _context;
        public BankRepository(DBContext context)
        {
            _context = context;
        }

        public bool CreateBank(Bank bank)
        {
            _context.Banks.Add(bank);
            return Save();
        }

        public Bank GetBankById(int bankId)
        {
            return _context.Banks.Where(b => b.Id == bankId).FirstOrDefault();
        }

        public List<Bank> GetBanks()
        {
            return _context.Banks.OrderBy(b => b.Id).ToList();
        }

        public bool HasBank(int bankId)
        {
            return _context.Banks.Any(b => b.Id == bankId);
        }

        public bool Save()
        {
            var saved=_context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public Bank checkBankByName(Bank bank)
        {
            return GetBanks().Where(c => c.Name.Trim().ToUpper() == bank.Name.TrimEnd().ToUpper()).FirstOrDefault();
        }

        public bool UpdateBank(Bank bank)
        {
            var bankUpdate=GetBankById(bank.Id);
                _context.Entry(bankUpdate).CurrentValues.SetValues(bank);
            return Save();
        }

        public bool DeleteBank(int bankId)
        {
            var bank = GetBankById(bankId);
            _context.Remove(bank);
            return Save();
        }

        public List<Bank> GetBanksByActive(bool active)
        {
            return _context.Banks.Where(b=> b.Active==active).OrderBy(b => b.Id).ToList();
        }
    }

}   
