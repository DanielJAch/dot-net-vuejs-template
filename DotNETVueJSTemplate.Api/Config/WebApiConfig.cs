using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

using Newtonsoft.Json.Serialization;
using Owin;

namespace DotNETVueJSTemplate.Api.Config
{
    public static class WebApiConfig
    {
        public static void ConfigureWebApi(this IAppBuilder app, HttpConfiguration config)
        {
            config.IncludeErrorDetailPolicy = GetErrorDetailPolicy();

            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // JSON serialization
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            // force 8601 date format
            var converter = new Newtonsoft.Json.Converters.IsoDateTimeConverter
            {
                DateTimeStyles = System.Globalization.DateTimeStyles.AdjustToUniversal
            };
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(converter);

            // CORS
            var enableCorsAttr = new EnableCorsAttribute($"{Constants.Urls.WebUrl}", "*", "*");
            enableCorsAttr.ExposedHeaders.Add("file-name");
            config.EnableCors(enableCorsAttr);
        }

        private static IncludeErrorDetailPolicy GetErrorDetailPolicy()
        {
            var config = (CustomErrorsSection)ConfigurationManager.GetSection("system.web/customErrors");

            IncludeErrorDetailPolicy errorDetailPolicy;

            switch (config.Mode)
            {
                case CustomErrorsMode.RemoteOnly:
                    errorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;
                    break;
                case CustomErrorsMode.On:
                    errorDetailPolicy = IncludeErrorDetailPolicy.Never;
                    break;
                case CustomErrorsMode.Off:
                    errorDetailPolicy = IncludeErrorDetailPolicy.Always;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return errorDetailPolicy;
        }
    }
}
