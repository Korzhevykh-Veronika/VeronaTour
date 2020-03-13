using Microsoft.Owin;
using Owin;
using VeronaTour.BLL.Configuration;

[assembly: OwinStartupAttribute(typeof(VeronaTour.WEB.Startup))]
namespace VeronaTour.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            StartupConfiguration.ConfigureIdentity(app);
        }
    }
}
