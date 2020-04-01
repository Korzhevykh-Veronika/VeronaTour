using Ninject.Modules;
using NLog;
using VeronaTour.BLL.Services;
using VeronaTour.BLL.Services.Interfaces;

namespace VeronaTour.WEB.Utils
{
    /// <summary>
    ///     Provides resolving of all Web project`s dependency injections
    /// </summary>
    public class WebModule : NinjectModule
    {
        /// <summary>
        ///     Loads resolvings of DI
        /// </summary>
        public override void Load()
        {
            Bind<IMainService>().To<MainService>().InSingletonScope();
            Bind<IOrdersService>().To<OrdersService>().InSingletonScope();
            Bind<IToursService>().To<ToursService>().InSingletonScope();
            Bind<IIdentityService>().To<IdentityService>().InSingletonScope();
            Bind<ILogger>().ToMethod(p => LogManager.GetCurrentClassLogger());
        }
    }
}