using System.Web.Http;

namespace OG_MFTG.HR_WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //format json to use camelCase
            //HttpConfiguration config = GlobalConfiguration.Configuration;
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            //    new CamelCasePropertyNamesContractResolver();
            //config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            //var formatters = GlobalConfiguration.Configuration.Formatters;
            //var jsonFormatter = formatters.JsonFormatter;
            //var settings = jsonFormatter.SerializerSettings;
            //settings.Formatting = Formatting.Indented;
            //settings.ContractResolver = new CamelCasePropertyNamesContractResolver();      \


            //GlobalConfiguration.Configuration
            //    .Formatters
            //    .JsonFormatter
            //    .SerializerSettings
            //    .ContractResolver = new CamelCasePropertyNamesContractResolver();


            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
