using System.Web.Http;
using ExileRota.Infrastructure.IoC;

namespace ExileRota.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AutofacConfig.Initialize();
        }
    }
}
