using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RememberUtility.Enum;
using RememberUtility.HandleUtil;
using RememberUtility.Model;

namespace BigRememberGUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserUtil _userUtil;

        public HomeController()
        {
            _userUtil = new UserUtil();
        }

        #region Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin userLogin)
        {
            Debug.WriteLine("[Login] Username: " + userLogin.Username);
            Debug.WriteLine("[Login] Password: " + userLogin.PasswordEncrypt);

            if (ModelState.IsValid)
            {
                var checkName = _userUtil.CheckUser(userLogin.Username, userLogin.PasswordEncrypt);
                if (checkName != null)
                {
                    Session["Name"] = checkName.Username;
                    Session["UserRole"] = checkName.UserRole;
                    // Create Cookies
                    var cookie = new HttpCookie("ForLogin");

                    // Save data at Cookies
                    HttpContext.Response.SetCookie(cookie);

                    System.Web.HttpContext.Current.Session["DetailUser"] = checkName;
                    System.Web.HttpContext.Current.Session["UserNameCookie"] = cookie.Value;

                    return RedirectToAction(checkName.UserRole == UserRoleEnum.NormalUser ? "TakePlace" : "Login");
                }
            }

            ViewBag.LoginFailed = "Occurred Error";
            return View();
        }

        // Still trouble with back button after logout
        [HttpGet]
        public ActionResult Logout()
        {
            var cookieLogout = Request.Cookies["ForLogin"];
            if (cookieLogout?.Value != null)
            {
                // Set Expired cookie
                Response.Buffer = true;
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
                Response.CacheControl = "no-cache";

                Response.Cache.SetNoStore();
                Response.Cache.SetExpires(DateTime.Now.AddHours(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                
                
                if (System.Web.HttpContext.Current.Session["DetailUser"] != null)
                {
                    Session.Abandon(); // Clear Session on the server
                    Session.Clear();
                    Session.RemoveAll();
                    FormsAuthentication.SignOut();
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Login", "Home");
        }

        #endregion

        [HttpGet]
        public ActionResult TakePlace()
        {
            return View();
        }

        public ActionResult GoBook()
        {
            return RedirectToAction("Index", "Books");
        }

        public ActionResult GoEntertainment()
        {
            return RedirectToAction("Index", "Entertainment");
        }

        public ActionResult GoQuote()
        {
            return RedirectToAction("Index", "Quotes");
        }

        public ActionResult GoEventInYear()
        {
            return RedirectToAction("Index", "EventInYears");
        }
    }
}