using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DBContext _context;

        public DepartmentRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> DeleteDepartmentAsync(int departmentId)
        {
            var department = await GetDepartmentByIdAsync(departmentId);
            if (department == null)
            {
                return null;

            }
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> GetDepartmentByIdAsync(int departmentId)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Id == departmentId);
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments.OrderBy(d => d.Id).ToListAsync();
        }

        public async Task<List<Department>> GetDepartmentsByActiveAsync(bool active)
        {
            return await _context.Departments.Include(p => p.InverseParentDepartment).Where(d => d.Active == active).ToListAsync();
        }


        public async Task<Department> UpdateDepartmentAsync(Department department)
        {
            var departmentUpdate = await GetDepartmentByIdAsync(department.Id);
            if (departmentUpdate == null)
            {
                return null;
            }
            _context.Entry(departmentUpdate).CurrentValues.SetValues(department);
            await _context.SaveChangesAsync();
            return department;
        }
    }
}
