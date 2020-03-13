using Ninject.Modules;
using VeronaTour.BLL.Services;
using VeronaTour.BLL.Services.Interfaces;

namespace VeronaTour.WEB.Utils
{
    public class WebModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMainService>().To<MainService>().InSingletonScope();
            Bind<IOrdersService>().To<OrdersService>().InSingletonScope();
            Bind<IToursService>().To<ToursService>().InSingletonScope();
            Bind<IIdentityService>().To<IdentityService>().InSingletonScope();
        }
    }
}