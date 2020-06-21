using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(bigSchool.Startup))]
namespace bigSchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
