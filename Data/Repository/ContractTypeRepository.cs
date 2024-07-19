using HumanManagement.Data.Repository.Interface;
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

        public bool DeleteContractType(int contractTypeId)
        {
            var contractType = GetContractTypeById(contractTypeId);

            if (contractType == null)
            {
                return false;
            }

            _context.Remove(contractType);
            return Save();
        }


        public List<ContractType> GetContractTypesByActive(bool active)
        {
            return _context.ContractTypes.Where(c => c.Active == active).ToList();
        }

        public ContractType GetContractTypeById(int contractTypeId)
        {
            return _context.ContractTypes.Where(c => c.Id == contractTypeId).FirstOrDefault();
        }

        public List<ContractType> GetContractTypes()
        {
            return _context.ContractTypes.OrderBy(c => c.Id).ToList();
        }

        public bool Save()
        {
            var check = _context.SaveChanges();
            return check > 0 ? true : false;
        }

        public bool UpdateContractType(ContractType contractType)
        {
            var contractTypeUpdate = GetContractTypeById(contractType.Id);
            if (contractTypeUpdate == null)
            {
                return false;
            }
            _context.Entry(contractTypeUpdate).CurrentValues.SetValues(contractType);
            return Save();
        }
    }
}
