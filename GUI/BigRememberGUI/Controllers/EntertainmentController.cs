using System.Net;
using System.Web.Mvc;
using ConnectionSampleCode.HandleUtil;
using ConnectionSampleCode.Model;

namespace BigRememberGUI.Controllers
{
    public class EntertainmentController : Controller
    {
        private readonly EntertainmentUtil _entertainmentUtil;

        public EntertainmentController()
        {
            _entertainmentUtil = new EntertainmentUtil();
        }
        // GET: Entertainment
        public ActionResult Index()
        {
            if (Session["Name"] != null)
            {
                return View(_entertainmentUtil.GetListEntertainments());
            }

            return RedirectToAction("Login", "Home");
        }

        // GET: Entertainment/Details/5
        public ActionResult Details(string id)
        {
            if (Session["Name"] == null) return RedirectToAction("Login", "Home");

            var getEtObj = _entertainmentUtil.FindEntertainmentByEnterId(id);
            return getEtObj != null ? View(getEtObj) : View();
        }

        // GET: Entertainment/Create
        public ActionResult Create()
        {
            if (Session["Name"] != null)
            {
                return View();
            }

            return RedirectToAction("Login", "Home");
        }

        // POST: Entertainment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnterId,EnterName,Links,Category,CreatedDate,LastModifiedDate")]Entertainment et)
        {
            if (!ModelState.IsValid) return View("Error");

            _entertainmentUtil.AddEntertainment(et);

            return RedirectToAction("Index", "Entertainment");
        }

        // GET: Entertainment/Edit/5
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session["Name"] == null) return RedirectToAction("Login", "Home");
            var tempEt = _entertainmentUtil.FindEntertainmentByEnterId(id);

            if (tempEt == null)
            {
                return HttpNotFound("Sorry '" + id + "' doesn't exist in our Db.");
            }

            return View(tempEt);
        }

        // POST: Entertainment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnterId,EnterName,Links,Category,CreatedDate,LastModifiedDate")]Entertainment et)
        {
            if (!ModelState.IsValid) return View();
            var currentEt = _entertainmentUtil.FindEntertainmentByEnterId(et.EnterId);

            _entertainmentUtil.UpdateEntertainment(currentEt.EnterName, et.EnterName, et.Links, et.Category);

            return RedirectToAction("Index", "Entertainment");
        }

        // GET: Entertainment/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var deleteEt = _entertainmentUtil.FindEntertainmentByEnterId(id);
            if (Session["Name"] == null || deleteEt == null) return RedirectToAction("Login", "Home");
            ViewBag.deleteName = deleteEt.EnterName;

            return View(deleteEt);
        }

        // POST: Entertainment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var entertainment = _entertainmentUtil.FindEntertainmentByEnterId(id);
            if (entertainment == null) return View("Error");
            _entertainmentUtil.DeleteEntertainment(entertainment.EnterName);
            return RedirectToAction("Index", "Entertainment");

        }
    }
}
