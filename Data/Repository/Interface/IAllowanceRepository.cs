using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IAllowanceRepository
    {
        List<Allowance> GetAllowances();
        Allowance GetAllowanceById(int allowanceId);
        List<Allowance> GetAllowancesByActive(bool active);
        bool Save();
        bool CreateAllowance(Allowance allowance);
        bool UpdateAllowance(Allowance allowance);
        bool DeleteAllowance(int allowanceId);
    }
}
