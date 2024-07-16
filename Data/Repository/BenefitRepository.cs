using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using System;

namespace HumanManagement.Data.Repository
{
    public class BenefitRepository: IBenefitRepository
    {
        private readonly DBContext _context;
        public BenefitRepository(DBContext dbContext)
        {
            _context = dbContext;
        }

        public bool CreateBenefit(Benefit benefit)
        {
            _context.Add(benefit);
            return Save();
        }

        public bool DeleteBenefit(int benefitId)
        {
            var benefitDelete=GetBenefitById(benefitId);
            _context.Remove(benefitDelete);
            return Save();
        }

        public List<Benefit> GetBenefitsByActive(bool active)
        {
            return _context.Benefits.Where(b=>b.Active==active).ToList();
        }

        public Benefit GetBenefitById(int benefitId)
        {
            return _context.Benefits.Where(b => b.Id == benefitId).FirstOrDefault();
        }

        public List<Benefit> GetBenefits()
        {
            return _context.Benefits.ToList();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateBenefit(Benefit benefit)
        {
            var benefitUpdate = GetBenefitById(benefit.Id);
            _context.Entry(benefitUpdate).CurrentValues.SetValues(benefit);
            return Save();
        }
    }
}
