using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TwitterMvcApp.Startup))]
namespace TwitterMvcApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
