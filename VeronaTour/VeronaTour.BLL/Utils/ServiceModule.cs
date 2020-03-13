using AutoMapper;
using Ninject.Modules;
using Ninject.Web.Common;
using VeronaTour.DAL;
using VeronaTour.DAL.EF;
using VeronaTour.DAL.Interfaces;

namespace VeronaTour.BLL.Utils
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            var configuration = VeronaMapperConfiguration.GetConfiguration();

            Bind<IMapper>().ToConstructor((c) => new Mapper(configuration)).InSingletonScope();
            Bind<VeronaTourDbContext>().ToSelf().InRequestScope();

            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}