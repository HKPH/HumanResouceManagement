using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;
namespace HumanManagement.Data.Repository
{
    public class EmployeeProcessRepository : IEmployeeProcessRepository
    {
        private readonly DBContext _context;

        public EmployeeProcessRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeProcess>> GetEmployeeProcessesAsync()
        {
            return await _context.EmployeeProcesses.OrderBy(ep => ep.Id).ToListAsync();
        }

        public async Task<EmployeeProcess> GetEmployeeProcessByIdAsync(int employeeProcessId)
        {
            return await _context.EmployeeProcesses.FindAsync(employeeProcessId);
        }

        public async Task<List<EmployeeProcess>> GetEmployeeProcessesByActiveAsync(bool active)
        {
            return await _context.EmployeeProcesses.Where(ep => ep.Active == active).ToListAsync();
        }

        public async Task<List<EmployeeProcess>> GetEmployeeProcessesByEmployeeIdAsync(int employeeId)
        {
            return await _context.EmployeeProcesses.Where(ep => ep.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<EmployeeProcess> CreateEmployeeProcessAsync(EmployeeProcess employeeProcess)
        {
            await _context.EmployeeProcesses.AddAsync(employeeProcess);
            await _context.SaveChangesAsync();
            return employeeProcess;
        }

        public async Task<EmployeeProcess> UpdateEmployeeProcessAsync(EmployeeProcess employeeProcess)
        {
            var employeeProcessUpdate = await GetEmployeeProcessByIdAsync(employeeProcess.Id);
            if (employeeProcessUpdate == null)
            {
                return null;
            }

            _context.Entry(employeeProcessUpdate).CurrentValues.SetValues(employeeProcess);
            await _context.SaveChangesAsync();
            return employeeProcessUpdate;
        }

        public async Task<EmployeeProcess> DeleteEmployeeProcessAsync(int employeeProcessId)
        {
            var employeeProcess = await GetEmployeeProcessByIdAsync(employeeProcessId);
            if (employeeProcess == null)
            {
                return null;
            }

            _context.EmployeeProcesses.Remove(employeeProcess);
            await _context.SaveChangesAsync();
            return employeeProcess;
        }
    }
}
