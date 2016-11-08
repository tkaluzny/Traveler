using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Traveler.Startup))]
namespace Traveler
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
