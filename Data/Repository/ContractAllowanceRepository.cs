using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class ContractAllowanceRepository : IContractAllowanceRepository
    {
        private readonly DBContext _context;
        public ContractAllowanceRepository(DBContext context)
        {
            _context = context;
        }
        public bool CreateContractAllowance(ContractAllowance contractAllowance)
        {
            _context.ContractAllowances.Add(contractAllowance);
            return Save();
        }

        public bool DeleteContractAllowance(int contractId, int allowanceId)
        {
            var contractAllowance= GetContractAllowance(contractId, allowanceId);
            _context.Remove(contractAllowance);
            return Save();
        }

        public List<Allowance> GetAllowancesByContract(int contractId)
        {
            return _context.ContractAllowances
                .Where(ca=>ca.ContractTypeId == contractId)
                .Include(ca=>ca.Allowance)
                .Select(ca=>ca.Allowance)
                .ToList();
        }

        public ContractAllowance GetContractAllowance(int contractId, int allowanceId)
        {
            return _context.ContractAllowances
                .Where(c => c.AllowanceId == allowanceId && c.ContractTypeId == contractId)
                .FirstOrDefault();

        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateContractAllowance(ContractAllowance contractAllowance)
        {
            var caUpdate=GetContractAllowance(contractAllowance.ContractTypeId, contractAllowance.AllowanceId);
            _context.Entry(caUpdate).CurrentValues.SetValues(contractAllowance);
            return Save();
        }
    }
}
