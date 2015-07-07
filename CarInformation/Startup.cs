using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarInformation.Startup))]
namespace CarInformation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
