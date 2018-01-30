using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZFC.Shop.Service;
using ZFC.Shop.Entity;
using ZFC.Shop.Utility;

namespace ZFC.Shop.Mobile.Controllers
{
    public class UserController : BaseController
    {
        readonly ICustomerService userService;
        public UserController(ICustomerService us)
        {
            userService = us;
        }

        public ActionResult Index()
        {
            return RedirectToAction("me");
        }

        // GET: User
        public ActionResult Me()
        {
            var user = base.User;
            ViewBag.UserExt = userService.GetCurrentUserExt();
            return View(user);
        }

        public ActionResult Logout()
        {
            var result = userService.Logout();
            return RedirectToAction("index", "login");
        }

        [HttpPost]
        public ActionResult LogoutPost()
        {
            var result = userService.Logout();
            return Json(result);
        }
    }
}