using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tumblelog.Startup))]
namespace Tumblelog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
