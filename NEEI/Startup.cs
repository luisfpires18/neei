using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NEEI.Startup))]
namespace NEEI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
