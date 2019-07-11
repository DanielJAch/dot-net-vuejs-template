using Owin;
using System.Web.Mvc;
using System.Web.Routing;

namespace DotNETVueJSTemplate.Api.Config
{
    public static class MvcConfig
    {
        public static void ConfigureMvc(this IAppBuilder app)
        {
            // areas
            AreaRegistration.RegisterAllAreas();

            // filters
            var filters = GlobalFilters.Filters;
            filters.Add(new HandleErrorAttribute());

            // routes
            var routes = RouteTable.Routes;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
