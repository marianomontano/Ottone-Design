using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GUI.Startup))]
namespace GUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
