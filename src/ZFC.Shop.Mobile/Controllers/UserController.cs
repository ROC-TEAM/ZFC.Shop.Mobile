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
    public class UserController : Controller
    {
        readonly IUserService uService;
        public UserController(/*IUserService us*/)
        {
            //uService = us;
        }

        public ActionResult Index()
        {
            return RedirectToAction("me");
        }

        // GET: User
        public ActionResult Me()
        {
            //var user = base.User;
            //var parentUser = uService.GetUserByWWID(user.ManagerWWID);
            //ViewBag.ParentUser = parentUser;
            return View();
        }

        [HttpPost]
        public ActionResult GetUserAutoList(string key)
        {
            var list = uService.GetList(10, key);
            return Content(JsonHelper.ToJson(list));
        }

        [HttpPost]
        public ActionResult ValidPwd(string wwid, string pwd)
        {
            var result = uService.ValidPwd(wwid, pwd);
            return Json(result);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            var result = uService.Logout();
            return Json(result);
        }
    }
}