using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using System;

namespace HumanManagement.Data.Repository
{
    public class EmployeeProcessRepository: IEmployeeProcessRepository
    {
        private readonly DBContext _context;
        public EmployeeProcessRepository(DBContext context)
        {
            _context = context;
        }

        public bool CreateEmployeeProcess(EmployeeProcess employeeProcess)
        {
            _context.Add(employeeProcess);
            return Save();
        }

        public bool DeleteEmployeeProcess(int employeeProcessId)
        {
            var ep=GetEmployeeProcessById(employeeProcessId);
            _context.Remove(ep);
            return Save();
        }

        public EmployeeProcess GetEmployeeProcessById(int employeeProcessId)
        {
            return _context.EmployeeProcesses.Where(ep=>ep.Id == employeeProcessId).FirstOrDefault();
        }

        public List<EmployeeProcess> GetEmployeeProcesses()
        {
            return _context.EmployeeProcesses.OrderBy(ep => ep.Id).ToList();
        }

        public List<EmployeeProcess> GetEmployeeProcessesByActive(bool active)
        {
            return _context.EmployeeProcesses.Where(ep=>ep.Active == active).ToList();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateEmployeeProcess(EmployeeProcess employeeProcess)
        {
            var epUpdate = GetEmployeeProcessById(employeeProcess.Id);
            _context.Entry(epUpdate).CurrentValues.SetValues(employeeProcess);
            return Save();
        }
    }
}
