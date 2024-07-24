﻿using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface ISalaryRepository
    {
        List<Salary> GetSalaries();
        Salary GetSalaryByEmployeeId(int employeeId);
        List<Salary> GetSalariesNotUsing();

        List<Salary> GetSalariesByActive(bool active);
        Salary GetSalaryById(int salaryId);
        bool CreateSalary(Salary salary);
        bool UpdateSalary(Salary salary);
        bool DeleteSalary(int salaryId);
        bool Save();
    }
}
