using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RentMovieProject_EF_API.Startup))]
namespace RentMovieProject_EF_API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
