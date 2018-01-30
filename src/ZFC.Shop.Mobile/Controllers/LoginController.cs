using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZFC.Shop.Entity;
using ZFC.Shop.Service;
using ZFC.Shop.Utility;

namespace ZFC.Shop.Mobile.Controllers
{
    public class LoginController : Controller
    {
        readonly ICustomerService uService;
        public LoginController(ICustomerService us)
        {
            uService = us;
        }
        // GET: Login
        public ActionResult Index(string redirectUrl = "")
        {
            bool isLogin = uService.IsLogin();
            if (isLogin)
            {
                return RedirectToAction("index", "user");
            }
            ViewBag.RedirectURL = redirectUrl;
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(string eid, string pwd, int remember)
        {
            var result = uService.Login(eid, pwd, remember == 1);
            return Json(result);
        }
    }
}