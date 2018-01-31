using Senparc.Weixin;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.OAuth2;
using Senparc.Weixin.QY.Containers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using ZFC.Shop.Utility;

namespace ZFC.Shop.Mobile.Controllers
{
    public class HomeController : Controller
    {
        public static readonly string CorpId = WebConfigurationManager.AppSettings["CorpId"];
        public static readonly string AppSecret = WebConfigurationManager.AppSettings["AppSecret"];
        public static readonly string AgentId = WebConfigurationManager.AppSettings["AgentId"];
        public static readonly string State = "EP_WECHAT_LOGIN_STATE";

        public ActionResult Index()
        {
            //string domain = WebConfigurationManager.AppSettings["Domain"];
            //string virtualPath = WebConfigurationManager.AppSettings["VirtualPath"];
            //string redirectUrl = string.Format("{0}{1}/home/redirectBack", domain, virtualPath);
            //string url = OAuth2Api.GetCode(CorpId, redirectUrl, State, AgentId);
            //var log = LogFactory.GetLogger();
            //log.Info("跳转URL", url);
            //return Redirect(url);

            return RedirectToAction("index", "vendor");
        }

        public ActionResult Msg()
        {
            return View();
        }

        public ActionResult Contract()
        {
            return View();
        }

        public ActionResult RedirectBack(string code, string state)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("code参数错误");
            }

            if (!string.Equals(state, State))
            {
                throw new ArgumentException("state参数错误");
            }

            string token = AccessTokenContainer.TryGetToken(CorpId, AppSecret);

            var result = OAuth2Api.GetUserId(token, code);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}