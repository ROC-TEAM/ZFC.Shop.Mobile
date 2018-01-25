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
        readonly IUserService uService;
        public LoginController(IUserService us)
        {
            uService = us;
        }
        // GET: Login
        public ActionResult Index(string redirectUrl = "")
        {
            bool isLogin = uService.IsLogin();
            if (isLogin)
            {
                return RedirectToAction("index", "main");
            }
            ViewBag.ActionURL = redirectUrl;
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(string eid, string pwd)
        {
            var result = uService.Login(eid, pwd);
            return Json(result);
        }
    }
}