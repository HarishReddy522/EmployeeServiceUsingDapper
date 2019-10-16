using EmployeeModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBusinessLayer
{
   public interface IDepartmentRepository
    {
        Department GetDepartmentById(int D_Id);
        List<Department> GetAllDepartments();
        int UpdateDepartment(Department DeptModel);
        int DeleteDepartment(int D_Id);
        int InsertDepartment(Department DeptModel);
    }
}
