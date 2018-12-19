using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PublisherScheduler01Service.Startup))]

namespace PublisherScheduler01Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}