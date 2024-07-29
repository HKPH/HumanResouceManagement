using HumanManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IDisciplineRepository
    {
        Task<List<Discipline>> GetDisciplinesAsync();
        Task<List<Discipline>> GetDisciplinesByEmployeeIdAsync(int employeeId);
        Task<Discipline> GetDisciplineByIdAsync(int disciplineId);
        Task<Discipline> CreateDisciplineAsync(Discipline discipline);
        Task<Discipline> UpdateDisciplineAsync(Discipline discipline);
        Task<Discipline> DeleteDisciplineAsync(int disciplineId);
    }
}
