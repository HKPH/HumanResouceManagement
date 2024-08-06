using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IAllowanceRepository
    {
        Task<List<Allowance>> GetAllowancesAsync();
        Task<Allowance> GetAllowanceByIdAsync(int allowanceId);
        Task<List<Allowance>> GetAllowancesByActiveAsync(bool active);
        Task<bool> SaveAsync();
        Task<Allowance> CreateAllowanceAsync(Allowance allowance);
        Task<Allowance> UpdateAllowanceAsync(Allowance allowance);
        Task<Allowance> DeleteAllowanceAsync(int allowanceId);
    }
}
