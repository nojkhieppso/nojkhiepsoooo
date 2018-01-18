using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeCinema.Genecode.Startup))]
namespace HomeCinema.Genecode
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
