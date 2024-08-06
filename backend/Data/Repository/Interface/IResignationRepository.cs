using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IResignationRepository
    {
        Task<List<Resignation>> GetResignationsAsync();
        Task<List<Resignation>> GetResignationsByAcceptedAsync(bool accepted);
        Task<Resignation> GetResignationByIdAsync(int resignationId);
        Task<Resignation> CreateResignationAsync(Resignation resignation);
        Task<Resignation> UpdateResignationAsync(Resignation resignation);
        Task<Resignation> DeleteResignationAsync(int resignationId);
    }
}
