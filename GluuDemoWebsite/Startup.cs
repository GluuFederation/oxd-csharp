using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GluuDemoWebsite.Startup))]
namespace GluuDemoWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
