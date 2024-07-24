using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IDisciplineRepository
    {
        List<Discipline> GetDisciplines();
        List<Discipline> GetDisciplinesByEmployeeId(int employeeId);
        Discipline GetDisciplineById(int disciplineId);

        bool Save();
        bool CreateDiscipline(Discipline discipline);
        bool UpdateDiscipline(Discipline discipline);
        bool DeleteDiscipline(int disciplineId);
    }
}
