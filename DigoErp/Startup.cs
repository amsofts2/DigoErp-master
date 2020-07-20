using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DigoErp.Startup))]
namespace DigoErp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
