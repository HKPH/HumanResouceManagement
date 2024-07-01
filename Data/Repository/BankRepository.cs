﻿using HumanManagement.Data;
using HumanManagement.Models;
using HumanManagement.Services.Interfaces;

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
            return _context.Banks.Where(b => b.BankId == bankId).FirstOrDefault();
        }

        public ICollection<Bank> GetBanks()
        {
            return _context.Banks.OrderBy(b => b.BankId).ToList();
        }

        public bool HasBank(int bankId)
        {
            return _context.Banks.Any(b => b.BankId == bankId);
        }

        public bool Save()
        {
            var saved=_context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}   
