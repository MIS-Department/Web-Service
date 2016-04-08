using System.Web.Http;
using OG_MFTG.DataLayer.Interfaces;
using OG_MFTG.DataLayer.Repositories;
using OG_MFTG.HR_WebApi;
using OG_MFTG.HR_WebApi.Util;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

[assembly: WebActivator.PostApplicationStartMethod(typeof(SimpleInjectorWebApiInitializer), "Initialize")]

namespace OG_MFTG.HR_WebApi
{
    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.EnableHttpRequestMessageTracking(GlobalConfiguration.Configuration);     
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
            container.RegisterSingleton<IRequestMessageProvider>(new RequestMessageProvider(container));

            
            container.Register<ICalculatedTimeRepository, CalculatedTimeRepository>(Lifestyle.Singleton);
            container.Register<IDailyTimeRecordRepository, DailyTimeRecordRepository>(Lifestyle.Singleton);
            container.Register<IEmployeeScheduleRepository, EmployeeScheduleRepository>(Lifestyle.Singleton);
            container.Register<IScheduleRepository, ScheduleRepository>(Lifestyle.Singleton);
            container.Register<ITemplateRepository, TemplateRepository>(Lifestyle.Singleton);
            container.Register<ITemplateScheduleRepository, TemplateScheduleRepository>(Lifestyle.Singleton);
            container.Register<ITimeCategoryRepository, TimeCategoryRepository>(Lifestyle.Singleton);
            container.Register<ITimeTypeRepository, TimeTypeRepository>(Lifestyle.Singleton);
            container.Register<IEmployeeRepository, EmployeeRepository>(Lifestyle.Transient);
        }
    }
}