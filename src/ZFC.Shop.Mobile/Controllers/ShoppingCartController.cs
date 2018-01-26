using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZFC.Shop.Mobile.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return RedirectToAction("cart");
        }

        public ActionResult Cart()
        {
            return View();
        }
    }
}