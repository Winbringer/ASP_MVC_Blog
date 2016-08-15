using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        // создаем контекст данных
        BookContext db = new BookContext();
        private bool notDisposed=true;

        protected override void Dispose(bool disposing)
        {
            if (notDisposed)
            {
                db?.Dispose();
                notDisposed = false;
            }
            base.Dispose(disposing);
        }
        // GET: Book
        public ActionResult Index()
        {
            var b = db.Books;
            return View(b);
        }

        [HttpGet]
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.Find(id);
            if (book != null)
            {
                return View(book);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}