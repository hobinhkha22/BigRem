using System.Net;
using System.Web.Mvc;
using ConnectionSampleCode.Constant;
using ConnectionSampleCode.Extension;
using ConnectionSampleCode.HandleUtil;
using ConnectionSampleCode.Model;

namespace BigRememberGUI.Controllers
{
    public class BooksController : Controller
    {
        private readonly BooksUtil _booksUtil;

        public BooksController()
        {
            _booksUtil = new BooksUtil();
        }

        // GET: Books
        public ActionResult Index()
        {
            if (Session["Name"] != null)
            {
                return View(_booksUtil.GetListBooks());
            }

            return RedirectToAction("Login", "Home");
        }

        // GET: Books/Details/5
        public ActionResult Details(string id)
        {
            if (Session["Name"] == null) return HttpNotFound("Oops. Something happened");

            var getBookObj = _booksUtil.FindBookByBookId(id);

            return getBookObj != null ? View(getBookObj) : View();
        }

        // GET: Books/Create
        [HttpGet]
        public ActionResult Create()
        {
            // Get list book categories constant
            var listConstantValue = typeof(CategoriesBookConstant).GetAllPublicConstantValues<string>();
            listConstantValue.Sort();

            ViewBag.ListBook = new SelectList(listConstantValue);

            if (Session["Name"] != null)
            {
                return View();
            }

            return RedirectToAction("Login", "Home");
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId, BookName, Author, Category, CreatedDate, LastModifiedDate")] Books book)
        {
            if (!ModelState.IsValid) return View("Error");
            _booksUtil.AddBook(book);

            return RedirectToAction("Index", "Books");
        }


        // GET: Books/Edit/5
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (Session["Name"] == null || id == null) return RedirectToAction("Login", "Home");
            var tempBook = _booksUtil.FindBookByBookId(id);
            if (tempBook == null)
            {
                return HttpNotFound("Sorry '" + id + "' doesn't exist in our Db.");
            }

            return View(tempBook);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId, BookName, Author, Category, CreatedDate, LastModifiedDate")] Books book)
        {
            if (!ModelState.IsValid) return View();
            var currentBookName = _booksUtil.FindBookByBookId(book.BookId);

            _booksUtil.UpdateBook(currentBookName.BookName, book.BookName, book.Author, book.Category);

            return RedirectToAction("Index", "Books");
        }

        // GET: Books/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var deleteBook = _booksUtil.FindBookByBookId(id);
            if (deleteBook == null) return HttpNotFound("There's no '" + id + "' in our Db");
            if (Session["Name"] == null) return RedirectToAction("Login", "Home");

            ViewBag.deleteBook = deleteBook.BookName;

            return View(deleteBook);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var book = _booksUtil.FindBookByBookId(id);
            if (book == null) return View("Error");

            _booksUtil.DeleteBook(book.BookName);

            return RedirectToAction("Index", "Books");

        }

    }
}
