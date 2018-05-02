using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MerchantDB.Startup))]
namespace MerchantDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
