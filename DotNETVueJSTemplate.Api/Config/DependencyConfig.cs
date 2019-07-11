using System.Web.Http;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using Owin;
using NLog;
using DotNETVueJSTemplate.Data;
using DotNETVueJSTemplate.Services;
using DotNETVueJSTemplate.Services.Email;
using DotNETVueJSTemplate.Services.Interfaces;

namespace DotNETVueJSTemplate.Api.Config
{
    public static class DependencyConfig
    {
        public static void ConfigureDependencies(this IAppBuilder app, HttpConfiguration httpConfig)
        {
            // Dependency Registration
            var container = new Container();

            container.Options.DefaultLifestyle = Lifestyle.Scoped;
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<ILogger>(() => LogManager.GetLogger("database"));

            container.Register(() => SetupHccDbContextWithLogging(Constants.ConnectionStringName, container.GetInstance<ILogger>()));

            // Register all data services here!!
            container.Register<IExampleEntityService, ExampleEntityService>();

            container.Register<IEmailSender, EmailSender>();

            container.RegisterWebApiControllers(httpConfig);
            container.Verify();

            // MVC Resolver
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            // Web API Resolver
            httpConfig.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        public static ExampleContext SetupHccDbContextWithLogging(string connectionString, ILogger logger = null)
        {
            var context = new ExampleContext(connectionString);

            logger = logger ?? LogManager.GetLogger("database");

            if (Constants.LogAllSqlCalls && logger != null)
            {
                context.Database.Log = msg => logger.SkipEmptyLogEntry(LogLevel.Trace, msg);
            }

            return context;
        }
    }
}
