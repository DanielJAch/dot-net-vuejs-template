using System;
using System.Threading.Tasks;
using System.Web.Cors;
using DotNETVueJSTemplate.Services.Dtos;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Owin;

namespace DotNETVueJSTemplate.Api.Config
{
    public static class SignalRConfig
    {
        public static void ConfigureSignalR(this IAppBuilder app)
        {
            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context =>
                    {
                        var policy = new CorsPolicy();

                        policy.Origins.Add(Constants.Urls.WebUrl);
                        policy.AllowAnyMethod = true;
                        policy.AllowAnyHeader = true;
                        policy.SupportsCredentials = true;

                        return Task.FromResult(policy);
                    }
                }
            };

            app.Map(Constants.Routes.SignalR, map =>
            {
                //map.UseCors(CorsOptions.AllowAll);
                map.UseCors(corsOptions);
                map.RunSignalR(new HubConfiguration() {EnableDetailedErrors = true});
            });

            GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => JsonSerializerFactory.Value);
        }

        private static readonly Lazy<JsonSerializer>
            JsonSerializerFactory = new Lazy<JsonSerializer>(GetJsonSerializer);

        private static JsonSerializer GetJsonSerializer()
        {
            return new JsonSerializer
            {
                ContractResolver = new FilteredCamelCasePropertyNamesContractResolver
                {
                    // 1) Register all types in specified assemblies:
                    //AssembliesToInclude =
                    //{
                    //    typeof (Startup).Assembly
                    //},

                    // 2) Register individual types:
                    TypesToInclude =
                    {
                        typeof(ExampleEntityDto),
                    }
                }
            };
        }
    }
}
