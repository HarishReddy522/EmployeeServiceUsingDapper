using System;
using System.Collections.Generic;
using System.Text;
using EmployeeModels;

namespace EmployeeBusinessLayer
{
   public interface IEmployeeRepository
    {
        EmployeeModel GetEmployeeById(int E_Id);
        List<EmployeeModel> GetAllEmployees();
        int UpdateEmployee(EmployeeModel EmpModel);
        int DeleteEmployee(int E_Id);
        int InsertEmployee(EmployeeModel EmpModel);

    }
}
