using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DapperDriven.Web.Startup))]
namespace DapperDriven.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
