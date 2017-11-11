using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_EATM.Startup))]
namespace MVC_EATM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
