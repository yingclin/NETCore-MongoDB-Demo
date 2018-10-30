using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMongo.Models;
using BookMongo.Models.BookMongo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace BookMongo.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookDataAccess _dataAccess;

        public BooksController(BookDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // GET: Books
        public ActionResult Index()
        {
            return View(_dataAccess.List());
        }

        // GET: Books/Details/5
        public ActionResult Details(string id)
        {
            return View(_dataAccess.Get(new ObjectId(id)));
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View(new Book());
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            try
            {
                _dataAccess.Create(book);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(book);
            }
        }

        // GET: Books/Edit/5
        public ActionResult Edit(string id)
        {
            return View(_dataAccess.Get(new ObjectId(id)));
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Book book)
        {
            book.Id = new ObjectId(id);

            try
            {
                _dataAccess.Update(book.Id, book);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(book);
            }
        }

        // GET: Books/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            return View(_dataAccess.Get(new ObjectId(id)));
        }

        // POST: Books/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            var docId = new ObjectId(id);

            try
            {
                _dataAccess.Remove(docId);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_dataAccess.Get(docId));
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}