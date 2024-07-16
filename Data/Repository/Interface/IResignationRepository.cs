using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IResignationRepository
    {
        List<Resignation> GetResignations();
        Resignation GetResignationById(int resignationId);
        bool Save();
        bool CreateResignation(Resignation resignation);
        bool UpdateResignation(Resignation resignation);
        bool DeleteResignation(int resignationId);
    }
}
