using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Caixa.Web.Startup))]
namespace Caixa.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
