using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartOrder.Startup))]
namespace SmartOrder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
