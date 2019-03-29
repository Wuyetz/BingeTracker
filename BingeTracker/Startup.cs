using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BingeTracker.Startup))]
namespace BingeTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
