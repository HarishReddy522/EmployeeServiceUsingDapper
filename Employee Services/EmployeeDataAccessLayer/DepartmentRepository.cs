using Dapper;
using EmployeeBusinessLayer;
using EmployeeModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EmployeeDataAccessLayer
{
    public class DepartmentRepository:IDepartmentRepository
    {
        IOptions<ReadConfig> _Connectionstring;
        public DepartmentRepository(IOptions<ReadConfig> Connectionstring)
        {
            _Connectionstring = Connectionstring;
        }

        public int DeleteDepartment(int D_Id)
        {
            using (var con = new SqlConnection(_Connectionstring.Value.Connectionstring))
            {
                string sql = "Delete from Employee where D_Id=@Id " +
                            "DELETE from Department where D_Id = @Id";
                               
                return con.Execute(sql, new { Id = D_Id });
            }
        }

        public List<Department> GetAllDepartments()
        {
            using (var con = new SqlConnection(_Connectionstring.Value.Connectionstring))
            {
                string sql = "select * from Department D LEFT Join Employee E on D.D_Id=E.D_Id order by D.D_Id;";
                var DeptDic = new Dictionary<int, Department>();
                List<Department> Deptlst = new List<Department>();
               //One To Many Mapping
                Deptlst= con.Query<Department, EmployeeModel, Department>(sql,
                    (Dept, Emp) =>
                    {
                        Department DepartmentEntry;
                        if (!(DeptDic.TryGetValue(Dept.D_Id, out DepartmentEntry)))
                        {
                            DepartmentEntry = Dept;
                            DepartmentEntry.employees = new List<EmployeeModel>();
                            DeptDic.Add(Dept.D_Id, Dept);
                        }
                        DepartmentEntry.employees.Add(Emp);
                        return DepartmentEntry;
                    },
                    splitOn: "E_Id").Distinct().ToList();
                //foreach (var D in Deptlst)
                //{
                //   List<EmployeeModel> emps= D.employees;
                //}


             ///   Deptlst= con.Query<Department>(StoredProdecure.GetAllDepartments, null, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
                return Deptlst;
            }


        }

        public Department GetDepartmentById(int DId)
        {
            using (var con = new SqlConnection(_Connectionstring.Value.Connectionstring))
            {
                var DeptDic = new Dictionary<int, Department>();
                Department dept = new Department();
                dept.employees = new List<EmployeeModel>();

                string sql = "select * from Department D LEFT Join Employee E on D.D_Id=E.D_Id where D.D_Id = @Id;";
                dept = con.Query<Department, EmployeeModel, Department>(sql,

                    (D, E) =>
                    {
                        if (dept.employees.Count() == 0)
                        {
                            dept = D;
                            dept.employees = new List<EmployeeModel>();
                        }
                        dept.employees.Add(E);
                        return dept;
                    },
                    splitOn: "E_Id",
                    param: new { Id = DId }
                   ).Distinct().FirstOrDefault(); ;
             
                return dept;
            }
        }

        public int InsertDepartment(Department DeptModel)
        {
            using (var con = new SqlConnection(_Connectionstring.Value.Connectionstring))
            {
                string sql = "INSERT into Department(DName, DLocation) values(@DName, @DLocation);";
                return con.Execute(sql, new { DName = DeptModel.DName, DLocation = DeptModel.DLOcation });
            }
        }

        public int UpdateDepartment(Department DeptModel)
        {
            
            using (var con = new SqlConnection(_Connectionstring.Value.Connectionstring))
            {
                string sql = "UPDATE Department SET DName=@DName, DLocation=@DLocation where D_Id = @Id;";
                return con.Execute(sql, new { Id = DeptModel.D_Id, DName = DeptModel.DName, DLocation= DeptModel.DLOcation });
            }
        }
    }
}
