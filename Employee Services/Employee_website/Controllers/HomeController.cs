using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Employee_website.Models;
using EmployeeBusinessLayer;

namespace Employee_website.Controllers
{
    public class HomeController : Controller
    {
        private IDepartmentRepository _IDepartmentRepository;
        public HomeController(IDepartmentRepository IDepartmentRepository)
        {
            _IDepartmentRepository = IDepartmentRepository;
        }
        public IActionResult Index()
        {
           var dlist= _IDepartmentRepository.GetAllDepartments();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
