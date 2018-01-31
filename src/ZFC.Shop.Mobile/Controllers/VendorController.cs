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
    public class VendorController : Controller
    {
        readonly IVendorService vendorService;
        public VendorController(IVendorService ivs)
        {
            vendorService = ivs;
        }


        public ActionResult Index(VendorQueryEntity model)
        {
            if (model == null) model = new VendorQueryEntity();

            var list = vendorService.GetVendorList(model);

            return View(list);
        }
    }
}