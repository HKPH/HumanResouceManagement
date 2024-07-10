using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public class ContractTypeRepository : IContractTypeRepository
    {
        private readonly DBContext _context;
        public ContractTypeRepository(DBContext context) { _context = context; }
        public bool CreateContractType(ContractType contractType)
        {
            _context.Add(contractType);
            return Save();
        }

        public bool DeleteContractType(ContractType contractType)
        {
            _context.Remove(contractType);
            return Save();
        }

        public ICollection<ContractType> GetContractTypeByActive(bool active)
        {
            return _context.ContractTypes.Where(c => c.Active==active).ToList();
        }

        public ContractType GetContractTypeById(int contractTypeId)
        {
            return _context.ContractTypes.Where(c => c.Id == contractTypeId).FirstOrDefault();
        }

        public ICollection<ContractType> GetContractTypes()
        {
            return _context.ContractTypes.OrderBy(c=>c.Id).ToList();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateContractType(ContractType contractType)
        {
            var contractTypeUpdate=GetContractTypeById(contractType.Id);
            _context.Entry(contractTypeUpdate).CurrentValues.SetValues(contractType);
            return Save();
        }
    }
}
