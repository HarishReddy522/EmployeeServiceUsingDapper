using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeBusinessLayer;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeModels;

namespace Employee_website.Controllers
{
    
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _IEmployeeRepository;
        private IDepartmentRepository _IDepartmentRepository;
        public EmployeeController(IEmployeeRepository IEmployeeRepository, IDepartmentRepository IDepartmentRepository)
        {
            _IEmployeeRepository = IEmployeeRepository;
            _IDepartmentRepository = IDepartmentRepository;
        }

        // GET: Employee
        public ActionResult Index()
        {
            var Emplist = _IEmployeeRepository.GetAllEmployees();
            return View(Emplist);
        }

        public ActionResult Create()
        {
            ViewBag.AllDepartments= GetDepartmentList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeModel Employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _IEmployeeRepository.InsertEmployee(Employee);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.AllDepartments = GetDepartmentList();
                    return View(Employee);
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.AllDepartments = GetDepartmentList();
            return View(_IEmployeeRepository.GetEmployeeById(id));
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeModel employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _IEmployeeRepository.UpdateEmployee(employee);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.AllDepartments = GetDepartmentList();
                    return View(employee);
                }

            }
            catch
            {
                ViewBag.AllDepartments = GetDepartmentList();
                return View(employee);
            }
        }

        public ActionResult Delete(int id)
        {
            return View(_IEmployeeRepository.GetEmployeeById(id));
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EmployeeModel employee)
        {
            try
            {
                _IEmployeeRepository.DeleteEmployee(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            EmployeeModel Employee = _IEmployeeRepository.GetEmployeeById(id);
            return View(Employee);
        }

        public List<SelectListItem> GetDepartmentList()
        {
            var Departmentlist = _IDepartmentRepository.GetAllDepartments();
            List<SelectListItem> departments = new List<SelectListItem>();
           // departments.Add(new SelectListItem { Text = "--select--" });
            departments = Departmentlist.Select(a => new SelectListItem()
            {
                Text = a.DName,
                Value = a.D_Id.ToString()
            }).ToList();
            return departments;
        }
    }
}