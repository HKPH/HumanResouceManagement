using HumanManagement.Models;
using HumanManagement.Models.Dto;

namespace HumanManagement.Data.Repository
{
    public class BankBranchRepository : IBankBranchRepository
    {
        private readonly DBContext _context;
        public BankBranchRepository(DBContext context) {
            _context=context;

        }
        public BankBranch checkBankBranchByName(BankBranchDto bankBranch)
        {
            return GetBankBranches().Where(c => c.Name.Trim().ToUpper() == bankBranch.Name.TrimEnd().ToUpper()).FirstOrDefault();
        }

        public bool CreateBankBranch(BankBranch bankBranch)
        {
            _context.Add(bankBranch);
            return Save();

        }
        public bool DeleteBankBranch(BankBranch bankBranch)
        {
            _context.Remove(bankBranch);
            return Save();
        }

        public bool DeleteBankBranches(List<BankBranch> bankBranches)
        {
            _context.RemoveRange(bankBranches);
            return Save();
        }

        public List<BankBranch> GetAllBankBranchesByBankId(int bankId)
        {
            return GetBankBranches().Where(b=> b.BankId== bankId).ToList();
        }

        public BankBranch GetBankBranchById(int bankBranchId)
        {
            var bankBranch=_context.BankBranches.Where(b=> b.BankBranchId == bankBranchId).FirstOrDefault();
            return bankBranch;
        }

        public ICollection<BankBranch> GetBankBranches()
        {
            return _context.BankBranches.OrderBy(b=> b.BankBranchId).ToList();
        }

        public bool HasBankBranch(int bankBranchId)
        {
            return _context.BankBranches.Any(b=> b.BankBranchId== bankBranchId);

        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateBankBranch(BankBranch bankBranch)
        {
            _context.Update(bankBranch);
            return Save();
        }
    }
}
