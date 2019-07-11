using System.Web.Mvc;
using System.Web.Routing;

namespace DotNETVueJSTemplate
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Auth",
                url: "auth/{action}",
                defaults: new { controller = "Auth" }
            );

            routes.MapRoute(
                name: "Error",
                url: "home/error",
                defaults: new { controller = "Home", action = "Error" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{*sparoute}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
