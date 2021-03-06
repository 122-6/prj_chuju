using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace prj_chuju
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Activity_page",
                url: "Activity/{tag}/{page}",
                defaults: new { controller = "Activity", action = "Index", tag = UrlParameter.Optional, page = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "BuildingList_page",
                url: "BuildingList/{page}",
                defaults: new { controller = "BuildingList", action = "Index", page = UrlParameter.Optional }
                );


            routes.MapRoute(
                name: "ActivityContent",
                url: "ActivityContent/{Id}",
                defaults: new { controller = "Activity", action = "ActivityContent", Id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "BuildingContent",
                url: "BuildingContent/{id}",
                defaults: new { controller = "BuildingList", action = "BuildingContent", Id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
