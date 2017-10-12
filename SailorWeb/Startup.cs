using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SailorWeb.Startup))]
namespace SailorWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}