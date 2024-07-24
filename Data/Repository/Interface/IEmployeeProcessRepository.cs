using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IEmployeeProcessRepository
    {
        List<EmployeeProcess> GetEmployeeProcesses();
        EmployeeProcess GetEmployeeProcessById(int employeeProcessId);
        List<EmployeeProcess> GetEmployeeProcessesByActive(bool active);
        List<EmployeeProcess> GetEmployeeProcessesByEmployeeId(int employeeId);
        bool CreateEmployeeProcess(EmployeeProcess employeeProcess);
        bool UpdateEmployeeProcess(EmployeeProcess employeeProcess);
        bool DeleteEmployeeProcess(int employeeProcessId);
        bool Save();
    }
}
