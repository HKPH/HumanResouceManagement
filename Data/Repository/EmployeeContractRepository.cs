using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanManagement.Data.Repository
{
    public class EmployeeContractRepository : IEmployeeContractRepository
    {
        private readonly DBContext _context;

        public EmployeeContractRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<EmployeeContract> CreateEmployeeContractAsync(EmployeeContract employeeContract)
        {
            await _context.EmployeeContracts.AddAsync(employeeContract);
            await _context.SaveChangesAsync();
            return employeeContract;
            
        }

        public async Task<EmployeeContract> DeleteEmployeeContractAsync(int employeeContractId)
        {
            var employeeContract = await GetEmployeeContractByIdAsync(employeeContractId);
            if (employeeContract != null)
            {
                return null;
            }
            _context.EmployeeContracts.Remove(employeeContract);
            await _context.SaveChangesAsync();
            return employeeContract;
        }

        public async Task<EmployeeContract> GetEmployeeContractByIdAsync(int employeeContractId)
        {
            return await _context.EmployeeContracts.FirstOrDefaultAsync(e => e.Id == employeeContractId);
        }

        public async Task<List<EmployeeContract>> GetEmployeeContractsAsync()
        {
            return await _context.EmployeeContracts.OrderBy(e => e.Id).ToListAsync();
        }

        public async Task<List<EmployeeContract>> GetEmployeeContractsByActiveAsync(bool active)
        {
            return await _context.EmployeeContracts.Where(e => e.Active == active).ToListAsync();
        }


        public async Task<EmployeeContract> UpdateEmployeeContractAsync(EmployeeContract employeeContract)
        {
            var employeeContractUpdate = await GetEmployeeContractByIdAsync(employeeContract.Id);
            if (employeeContractUpdate == null)
            {
                return null;
               
            }
            _context.Entry(employeeContractUpdate).CurrentValues.SetValues(employeeContract);
            await _context.SaveChangesAsync();
            return employeeContract;
        }

        public async Task<List<Employee>> GetNewEmployeesAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.EmployeeContracts
                .Where(ec => ec.StartDate.HasValue
                            && ec.StartDate.Value >= startDate
                            && ec.StartDate.Value <= endDate
                            && ec.Active == true)
                .Select(ec => ec.Employee)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Employee>> GetTerminatedEmployeesAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.EmployeeContracts
                .Where(ec => ec.EndDate.HasValue
                            && ec.EndDate.Value >= startDate
                            && ec.EndDate.Value <= endDate
                            && ec.Active == false)
                .Select(ec => ec.Employee)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeesByDateAsync(DateTime date)
        {
            return await _context.EmployeeContracts
                .Where(ec => ec.EndDate.HasValue
                            && ec.EndDate.Value >= date
                            && ec.Active == false)
                .Select(ec => ec.Employee)
                .Distinct()
                .ToListAsync();
        }
    }
}
