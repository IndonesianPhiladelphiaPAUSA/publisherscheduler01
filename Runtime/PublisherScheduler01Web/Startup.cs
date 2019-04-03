using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PublisherScheduler01Web.Startup))]
namespace PublisherScheduler01Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // ConfigureMobileApp(app);
        }
    }
}
