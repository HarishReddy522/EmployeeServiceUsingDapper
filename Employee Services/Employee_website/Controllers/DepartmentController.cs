using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeBusinessLayer;
using EmployeeModels;
using Microsoft.AspNetCore.Mvc;

namespace Employee_website.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentRepository _IDepartmentRepository;
        public DepartmentController(IDepartmentRepository IDepartmentRepository)
        {
            _IDepartmentRepository = IDepartmentRepository;
        }
        public IActionResult Index()
        {
            List<Department> Dlist = _IDepartmentRepository.GetAllDepartments();
            return View(Dlist);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department Department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _IDepartmentRepository.InsertDepartment(Department);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(Department);
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(_IDepartmentRepository.GetDepartmentById(id));
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department Department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _IDepartmentRepository.UpdateDepartment(Department);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(Department);
                }

            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            return View(_IDepartmentRepository.GetDepartmentById(id));
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Department Department)
        {
            try
            {
                _IDepartmentRepository.DeleteDepartment(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int D_id)
        {
            Department Department= _IDepartmentRepository.GetDepartmentById(D_id);
            return View(Department);
        }
        [HttpPost]
        public IActionResult Search(string search)
        {
            List<Department> Dlist = _IDepartmentRepository.GetAllDepartments();
            if (!string.IsNullOrEmpty(search))
                return PartialView("_DepartmentList", Dlist.Where(t => t.DName.ToLower().Contains(search.ToLower()) || t.DLOcation.ToLower().Contains(search.ToLower())).ToList());
            else
                return PartialView("_DepartmentList", Dlist);
        }
    }
}