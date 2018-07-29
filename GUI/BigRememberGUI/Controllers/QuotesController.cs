using System.Net;
using System.Web.Mvc;
using ConnectionSampleCode.Constant;
using ConnectionSampleCode.Extension;
using ConnectionSampleCode.HandleUtil;
using ConnectionSampleCode.Model;

namespace BigRememberGUI.Controllers
{
    public class QuotesController : Controller
    {
        private readonly QuoteUtil _quoteUtil;

        public QuotesController()
        {
            _quoteUtil = new QuoteUtil();
        }

        // GET: Quotes
        public ActionResult Index()
        {
            if (Session["Name"] != null)
            {
                return View(_quoteUtil.GetListQuotes());
            }

            return RedirectToAction("Login", "Home");
        }

        // GET: Quotes/Details/5
        public ActionResult Details(string id)
        {
            if (Session["Name"] == null || id == null) return RedirectToAction("Login", "Home");

            var getQuote = _quoteUtil.FindQuoteByQuoteId(id);

            return getQuote != null ? View(getQuote) : View();
        }

        // GET: Quotes/Create
        public ActionResult Create()
        {
            var listConstantValue = typeof(TypesQuoteConstant).GetAllPublicConstantValues<string>();
            listConstantValue.Sort();
            ViewBag.ListQuotes = new SelectList(listConstantValue);

            if (Session["Name"] != null)
            {
                return View();
            }

            return RedirectToAction("Login", "Home");
        }

        // POST: Quotes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuotesId,QuotesName,Author,Type,CreatedDate,LastModifiedDate")]Quotes quote)
        {
            if (!ModelState.IsValid) return View("Error");

            _quoteUtil.AddQuote(quote);

            return RedirectToAction("Index", "Quotes");
        }

        // GET: Quotes/Edit/5
        public ActionResult Edit(string id)
        {
            var listConstantValue = typeof(TypesQuoteConstant).GetAllPublicConstantValues<string>();
            listConstantValue.Sort();
            ViewBag.EditListQuotes = new SelectList(listConstantValue);

            if (Session["Name"] == null || id == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["Name"] == null) return RedirectToAction("Login", "Home");
            var tempQuote = _quoteUtil.FindQuoteByQuoteId(id);

            if (tempQuote == null)
            {
                return HttpNotFound("Sorry '" + id + "' doesn't exist in our Db.");
            }

            return View(tempQuote);
        }

        // POST: Quotes/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "QuotesId,QuotesName,Author,Type,CreatedDate,LastModifiedDate")]Quotes quote)
        {
            if (!ModelState.IsValid) return View();

            var currentQuote = _quoteUtil.FindQuoteByQuoteId(quote.QuotesId);
            _quoteUtil.UpdateQuote(currentQuote.QuotesName, quote.QuotesName, quote.Author, quote.Type);

            return RedirectToAction("Index");
        }

        // GET: Quotes/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["Name"] == null || id == null) return RedirectToAction("Login", "Home");

            var deleteQuote = _quoteUtil.FindQuoteByQuoteId(id);
            ViewBag.deleteQuote = deleteQuote.QuotesName;

            return View(deleteQuote);
        }

        // POST: Quotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var quote = _quoteUtil.FindQuoteByQuoteId(id);
            if (quote == null) return View("Error");

            _quoteUtil.DeleteQuote(quote.QuotesName);

            return RedirectToAction("Index", "Quotes");

        }
    }
}
