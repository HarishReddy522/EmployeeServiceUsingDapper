using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeBusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee_website.Controllers
{
    public class OrderController : Controller
    {
        private IOrdersItemsRepository _IOrdersItemsRepository;
        public OrderController(IOrdersItemsRepository IOrdersItemsRepository)
        {
            _IOrdersItemsRepository = IOrdersItemsRepository;
        }
        // GET: Order
        public ActionResult Index()

        {
            var orderlst = _IOrdersItemsRepository.GetAllOrders();
            return View(orderlst);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View(_IOrdersItemsRepository.GetAllOrders().Where(O=>O.OrId==id).FirstOrDefault());
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}