using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ConnectionSampleCode.Constant;
using ConnectionSampleCode.Extension;
using ConnectionSampleCode.HandleUtil;
using ConnectionSampleCode.Model;
using PagedList;
using static System.String;

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
        public ActionResult Index(string sortingOrder, string searchData, string filterValue, int? pageNo)
        {
            if (Session["Name"] != null)
            {
                ViewBag.CurrentSortOrder = sortingOrder;
                ViewBag.BookId = IsNullOrEmpty(sortingOrder) ? "Book Id" : "";
                ViewBag.BookName = IsNullOrEmpty(sortingOrder) ? "Book name" : "";
                ViewBag.Author = IsNullOrEmpty(sortingOrder) ? "Author" : "";
                ViewBag.Category = IsNullOrEmpty(sortingOrder) ? "Category" : "";
                ViewBag.CreatedDate = IsNullOrEmpty(sortingOrder) ? "Created date" : "";
                ViewBag.LastModifiedDate = IsNullOrEmpty(sortingOrder) ? "Last modified date" : "";

                if (searchData != null)
                {
                    pageNo = 1; // default is 1
                }
                else
                {
                    searchData = filterValue;
                }

                //filter
                ViewBag.FilterValue = searchData;

                IEnumerable<Books> books = _booksUtil.GetListBooks();

                if (!IsNullOrEmpty(searchData))
                {
                    books = books.Where(b =>
                        b.BookName.ToUpper().Contains(searchData.ToUpper())
                        || b.Author.ToUpper().Contains(searchData.ToUpper())
                        || b.Category.ToUpper().Contains(searchData.ToUpper())
                        || b.CreatedDate.ToUpper().Contains(searchData.ToUpper())
                        || b.LastModifiedDate.ToUpper().Contains(searchData.ToUpper()));
                }

                switch (sortingOrder)
                {
                    case "Book Id":
                        books = (books ?? throw new InvalidOperationException()).OrderBy(b => b.BookId);
                        break;
                    case "Book name":
                        books = (books ?? throw new InvalidOperationException()).OrderBy(b => b.BookName);
                        break;
                    case "Author":
                        books = (books ?? throw new InvalidOperationException()).OrderBy(b => b.Author);
                        break;
                    case "Category":
                        books = (books ?? throw new InvalidOperationException()).OrderBy(b => b.Category);
                        break;
                    case "Created date":
                        books = (books ?? throw new InvalidOperationException()).OrderBy(b => b.CreatedDate);
                        break;
                    case "Last modified date":
                        books = (books ?? throw new InvalidOperationException()).OrderBy(b => b.LastModifiedDate);
                        break;
                    default:
                        books = (books ?? throw new InvalidOperationException()).OrderByDescending(b => b.BookName);
                        break;
                }

                const int sizeOfPage = 7; // records number show in 1 page
                var noOfPage = (pageNo ?? 1);  // page default is 1
                return View(books.ToPagedList(noOfPage, sizeOfPage));
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
            if (!ModelState.IsValid) return View(book);
            if (Session["Name"] == null || book.BookId == null)
            {
                return View(book);
            }

            _booksUtil.AddBook(book);

            return RedirectToAction("Index", "Books");
        }


        // GET: Books/Edit/5
        [HttpGet]
        public ActionResult Edit(string id)
        {
            // Get list book categories constant
            var listConstantValue = typeof(CategoriesBookConstant).GetAllPublicConstantValues<string>();
            listConstantValue.Sort();
            ViewBag.EditListBook = listConstantValue;

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
            if (Session["Name"] == null || id == null)
            {
                return RedirectToAction("Login", "Home");
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
