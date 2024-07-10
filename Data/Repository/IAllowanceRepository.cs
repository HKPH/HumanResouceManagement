using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public interface IAllowanceRepository
    {
        ICollection<Allowance> GetAllowances();
        Allowance GetAllowanceById(int allowanceId);
        ICollection<Allowance> GetAllowanceByActive(bool active);
        bool Save();
        bool CreateAllowance(Allowance allowance);
        bool UpdateAllowance(Allowance allowance);
        bool DeleteAllowance(Allowance allowance);
    }
}
