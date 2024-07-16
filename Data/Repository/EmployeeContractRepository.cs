using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class EmployeeContractRepository : IEmployeeContractRepository
    {
        private readonly DBContext _context;
        public EmployeeContractRepository(DBContext context)
        {
            _context = context;
        }

        public bool CreateEmployeeContract(EmployeeContract employeeContract)
        {
            _context.Add(employeeContract);
            return Save();
        }

        public bool DeleteEmployeeContract(int employeeContractId)
        {
            var employeeContract=GetEmployeeContractById(employeeContractId);
            _context.Remove(employeeContract);
            return Save();
        }

        public EmployeeContract GetEmployeeContractById(int employeeContractId)
        {
            return _context.EmployeeContracts.Where(e => e.Id == employeeContractId).FirstOrDefault();
        }

        public List<EmployeeContract> GetEmployeeContractsByActive(bool active)
        {
            return _context.EmployeeContracts.Where(e=>e.Active == active).ToList();
        }

        public List<EmployeeContract> GetEmployeeContracts()
        {
            return _context.EmployeeContracts.OrderBy(e=>e.Id).ToList();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateEmployeeContract(EmployeeContract employeeContract)
        {
            var employeeContractUpdate=GetEmployeeContractById(employeeContract.Id);
            _context.Entry(employeeContractUpdate).CurrentValues.SetValues(employeeContract);
            return Save();
        }
    
    }
}
