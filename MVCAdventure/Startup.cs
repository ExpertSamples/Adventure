using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCAdventure.Startup))]
namespace MVCAdventure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
