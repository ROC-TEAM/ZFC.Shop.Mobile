using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace ZFC.Shop.Mobile
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Product_List",
               url: "product/{VendorId}",
               defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
               constraints: new { VendorId = @"^[\d]{1,12}$" },
               namespaces: new string[] { "ZFC.Shop.Mobile.Controllers" }
           );

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "ZFC.Shop.Mobile.Controllers" }
           );
        }
    }
}
