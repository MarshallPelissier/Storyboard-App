using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Storyboard_App.Startup))]
namespace Storyboard_App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
