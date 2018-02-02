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
    public class ProductController : BaseFrontPageController
    {
        readonly IProductService productService;

        public ProductController(IProductService ips)
        {
            productService = ips;
        }

        public ActionResult Index(ProductQueryEntity model)
        {
            //if (model == null) model = new ProductQueryEntity();
            //var list = productService.GetProductList(model);
            ViewBag.QueryModel = model;

            return View();
        }

        [HttpPost]
        public ActionResult GetProductList(ProductQueryEntity model)
        {
            if (model == null) model = new ProductQueryEntity();

            var list = productService.GetProductList(model);

            return Json(new { Data = list, PageCount = model.GetTotalPageCount() });
        }
    }
}