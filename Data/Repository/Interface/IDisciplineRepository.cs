using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IDisciplineRepository
    {
        List<Discipline> GetDisciplines();
        Discipline GetDisciplineById(int disciplineId);
        bool Save();
        bool CreateDiscipline(Discipline discipline);
        bool UpdateDiscipline(Discipline discipline);
        bool DeleteDiscipline(int disciplineId);
    }
}
