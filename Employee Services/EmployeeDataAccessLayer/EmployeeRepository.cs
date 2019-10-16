using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EmployeeBusinessLayer;
using EmployeeModels;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using Dapper;

namespace EmployeeDataAccessLayer
{
        public class EmployeeRepository: IEmployeeRepository
    {
        IOptions<ReadConfig> _Connectionstring;
        public EmployeeRepository(IOptions<ReadConfig> Connectionstring)
        {
            _Connectionstring = Connectionstring;
        }

        public EmployeeModel GetEmployeeById(int E_Id)
        {
           
                using (var con = new SqlConnection(_Connectionstring.Value.Connectionstring))
                {
                    string sql = "Select * from Employee Emp INNER JOIN Department Dep ON Emp.D_Id = Dep.D_Id where E_Id = @Id;";
                    return con.Query<EmployeeModel,Department,EmployeeModel > (sql,
                        map: (emp, Dept) => {
                        emp.Department = Dept;
                        return emp;
                          },
                        splitOn: "D_Id",
                        param:  new { Id = E_Id }).FirstOrDefault();
                }
            
        }

        public List<EmployeeModel> GetAllEmployees()
        {
            using (var Con=new SqlConnection(_Connectionstring.Value.Connectionstring))
            {
                List<EmployeeModel> emplist = new List<EmployeeModel>();
                emplist = Con.Query<EmployeeModel,Department,EmployeeModel>(StoredProdecure.GetAllEmployees,
                    map:(emp,Dept)=> {
                        emp.Department = Dept;
                        return emp;
                        },
                    splitOn:"D_Id",
                    param:null,
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
                return emplist;
            }
        }

        public int UpdateEmployee(EmployeeModel EmpModel)
        {
            using (var con = new SqlConnection(_Connectionstring.Value.Connectionstring))
            {
                string sql = "Update Employee Set EName =@EName , PhoneNumber = @PhoneNumber, salary = @salary, D_Id =@D_Id where E_Id = @Id;";
                return con.Execute(sql, new { Id = EmpModel.E_Id, EName = EmpModel.EName, PhoneNumber = EmpModel.PhoneNumber, salary=EmpModel.salary,D_id=EmpModel.Department.D_Id });
            }

        }

        public int DeleteEmployee(int E_Id)
        {
            using (var con = new SqlConnection(_Connectionstring.Value.Connectionstring))
            {
                string sql = "DELETE from Employee where E_Id = @Id;";
                return con.Execute(sql, new { Id = E_Id });
            }
        }

        public int InsertEmployee(EmployeeModel EmpModel)
        {

            using (var con = new SqlConnection(_Connectionstring.Value.Connectionstring))
            {
                string sql = " Insert into Employee(EName, PhoneNumber, salary, D_Id) values(@EName,@PhoneNumber,@salary,@D_Id);";
                return con.Execute(sql, new { EName = EmpModel.EName, PhoneNumber = EmpModel.PhoneNumber, salary=EmpModel.salary, D_Id=EmpModel.Department.D_Id });
            }
           
           
        }
    }
}
