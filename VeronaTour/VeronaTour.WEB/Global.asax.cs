using Homework_mvc.Util;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VeronaTour.BLL.Utils;
using VeronaTour.WEB.Utils;

namespace VeronaTour.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var kernel = new StandardKernel(new ServiceModule(), new WebModule());

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
