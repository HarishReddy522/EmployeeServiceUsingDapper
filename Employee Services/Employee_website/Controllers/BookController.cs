using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeBusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee_website.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository _IBookRepository;
        public BookController(IBookRepository IBookRepository)
        {
            _IBookRepository = IBookRepository;
        }
        // GET: Book
        public ActionResult Index()
        {
            return View(_IBookRepository.GetAllBooksWithDetails());
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            return View(_IBookRepository.GetAllBooksWithDetails().Where(B=>B.BId==id).FirstOrDefault());
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            _IBookRepository.GetAllUserLibraryDetails();

            return View();
        }

        // POST: Book/Create
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

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Book/Edit/5
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

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Book/Delete/5
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