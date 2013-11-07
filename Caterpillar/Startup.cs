using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Caterpillar.Startup))]
namespace Caterpillar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
