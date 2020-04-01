using System.Web.Mvc;
using System.Web.Routing;

namespace VeronaTour.WEB
{
    /// <summary>
    ///     Configures application routes
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        ///     Adds predefined routes to an existing route collection
        /// </summary>
        /// <param name="routes">Routes collection</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "{controller}/{action}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
