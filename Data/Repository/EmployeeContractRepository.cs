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
    }
}
