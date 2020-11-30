using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DeepChecks.WebMVC.Startup))]
namespace DeepChecks.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
