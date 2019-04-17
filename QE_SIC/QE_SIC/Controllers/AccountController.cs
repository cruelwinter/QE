using QE.Models;
using QE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QE.Controllers
{
    public class AccountController : Controller
    {

        // GET: Index
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return Login();
        }

        // GET: Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost, ActionName("Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPost(QE_USER user)
        {
            if (!string.IsNullOrEmpty(user.USER_ID) && !string.IsNullOrEmpty(user.PASSWORD))
            {
                ClientSessionService.Login(user);

                if (ClientSessionService.IsLogined)
                    return RedirectToAction("Index", "AdminAndSetup");
                
            }
            TempData["msg"] = "User ID or Password invalid!";
            return Index();

        }

        // GET: Logout
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            ClientSessionService.Logout();
            return RedirectToAction("Login", "Account");
        }

    }
}