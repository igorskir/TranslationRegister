using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TranslationReg.Startup))]
namespace TranslationReg
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
