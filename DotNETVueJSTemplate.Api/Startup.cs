using System.Web.Http;
using Microsoft.Owin;
using Owin;
using NLog;
using DotNETVueJSTemplate.Api;
using DotNETVueJSTemplate.Api.Config;

[assembly: OwinStartup(typeof(Startup))]
namespace DotNETVueJSTemplate.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfig = new HttpConfiguration();

            // Custom middleware for setting up trace logging
            app.Use<ExampleMiddleware>();

            // SignalR Config
            app.ConfigureSignalR();

            // Mvc Config
            app.ConfigureMvc();

            // Web API Config
            app.ConfigureWebApi(httpConfig);

            // DI Config
            app.ConfigureDependencies(httpConfig);

            app.UseWebApi(httpConfig);

            // Flush the log when the App is closing
            app.OnDisposing(LogManager.Flush);

            SeedData.SeedExampleEntities();
        }
    }
}