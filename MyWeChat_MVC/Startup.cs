using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyWeChat_MVC.Startup))]
namespace MyWeChat_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
