using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace HumanManagement.Data.Repository
{
    public class ContractTypeRepository : IContractTypeRepository
    {
        private readonly DBContext _context;

        public ContractTypeRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<ContractType> CreateContractTypeAsync(ContractType contractType)
        {
            await _context.ContractTypes.AddAsync(contractType);
            await SaveAsync();
            return contractType;
        }

        public async Task<ContractType> DeleteContractTypeAsync(int contractTypeId)
        {
            var contractType = await GetContractTypeByIdAsync(contractTypeId);
            if (contractType == null)
            {
                return null;
            }

            _context.ContractTypes.Remove(contractType);
            await SaveAsync();
            return contractType;
        }

        public async Task<List<ContractType>> GetContractTypesByActiveAsync(bool active)
        {
            return await _context.ContractTypes.Where(c => c.Active == active).ToListAsync();
        }

        public async Task<ContractType> GetContractTypeByIdAsync(int contractTypeId)
        {
            return await _context.ContractTypes.FirstOrDefaultAsync(c => c.Id == contractTypeId);
        }

        public async Task<List<ContractType>> GetContractTypesAsync()
        {
            return await _context.ContractTypes.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var check = await _context.SaveChangesAsync();
            return check > 0;
        }

        public async Task<ContractType> UpdateContractTypeAsync(ContractType contractType)
        {
            var contractTypeUpdate = await GetContractTypeByIdAsync(contractType.Id);
            if (contractTypeUpdate == null)
            {
                return null;
            }
            _context.Entry(contractTypeUpdate).CurrentValues.SetValues(contractType);
            await SaveAsync();
            return contractType;
        }
    }
}
