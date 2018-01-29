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
        readonly ICustomerService user;
        public UserController(ICustomerService us)
        {
            user = us;
        }

        public ActionResult Index()
        {
            return RedirectToAction("me");
        }

        // GET: User
        public ActionResult Me()
        {
            var user = base.User;
            return View(user);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            var result = user.Logout();
            return Json(result);
        }
    }
}