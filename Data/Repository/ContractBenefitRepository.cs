using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class ContractBenefitRepository: IContractBenefitRepository
    {
        private readonly DBContext _context;
        public ContractBenefitRepository(DBContext context)
        {
            _context = context;
        }

        public bool CreateContractBenefit(ContractBenefit contractBenefit)
        {
            _context.Add(contractBenefit);
            return Save();
        }

        public bool DeleteContractBenefit(int contractId, int benefitId)
        {
            var cd=GetContractBenefit(contractId, benefitId);
            _context.Remove(cd);
            return Save();
        }

        public List<Benefit> GetBenefitsByContract(int contractId)
        {
            return _context.ContractBenefits
                .Where(cb=>cb.ContractTypeId == contractId)
                .Include(cb=>cb.Benefit)
                .Select(cb=>cb.Benefit)
                .ToList();
        }

        public ContractBenefit GetContractBenefit(int contractId, int benefitId)
        {
            return _context.ContractBenefits
                .Where(cb => cb.ContractTypeId == contractId && cb.BenefitId == benefitId)
                .FirstOrDefault();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateContractBenefit(ContractBenefit contractBenefit)
        {
            var cbUpdate=GetContractBenefit(contractBenefit.ContractTypeId, contractBenefit.BenefitId);
            _context.Entry(cbUpdate).CurrentValues.SetValues(contractBenefit);
            return Save();
        }
    }
}
