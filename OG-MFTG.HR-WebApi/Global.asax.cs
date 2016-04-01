using System.Web.Http;

namespace OG_MFTG.HR_WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //SimpleInjectorWebApiInitializer.Initialize();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
