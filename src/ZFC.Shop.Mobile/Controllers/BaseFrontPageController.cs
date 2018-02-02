using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ZFC.Shop.Mobile.Controllers
{
    public class BaseFrontPageController : Controller
    {
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return this.Json(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet);
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            MyJsonResult json = new MyJsonResult(false);
            json.Data = data;
            json.ContentEncoding = contentEncoding;
            json.ContentType = contentType;
            json.JsonRequestBehavior = behavior;
            return json;
        }
    }
}