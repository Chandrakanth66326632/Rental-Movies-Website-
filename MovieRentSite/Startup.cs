using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieRentSite.Startup))]
namespace MovieRentSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
