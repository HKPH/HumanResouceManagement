using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HumanManagement.Data.Repository
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private readonly DBContext _context;
        public DepartmentRepository(DBContext context) {
            _context = context; }

        public bool CreateDepartment(Department department)
        {
            _context.Add(department);
            return Save();
        }

        public bool DeleteDepartment(int departmentId)
        {
            var department=GetDepartmentById(departmentId);
            _context.Remove(department);
            return Save();
        }

        public Department GetDepartmentById(int departmentId)
        {
            return _context.Departments.Where(d => d.Id == departmentId).FirstOrDefault();
        }

        public List<Department> GetDepartments()
        {
            return _context.Departments.OrderBy(d => d.Id).ToList();
        }

        public List<Department> GetDepartmentsByActive(bool active)
        {
            return _context.Departments.Include(p=>p.InverseParentDepartment).Where(d=>d.Active==active).ToList();
        }

        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateDepartment(Department department)
        {
            var departmentUpdate = GetDepartmentById(department.Id);
            _context.Entry(departmentUpdate).CurrentValues.SetValues(department);
            return Save();
        }
    }
}
