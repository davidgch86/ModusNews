using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsClient.Startup))]
namespace NewsClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
