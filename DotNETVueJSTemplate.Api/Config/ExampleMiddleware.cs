using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.BuilderProperties;
using Owin;

namespace DotNETVueJSTemplate.Api.Config
{
    internal class ExampleMiddleware : OwinMiddleware
    {
        public ExampleMiddleware(OwinMiddleware next) : base(next) { }

        public override async Task Invoke(IOwinContext context)
        {
            HttpContext.Current.Items[Constants.HttpRequestGiudKey] =
                Trace.CorrelationManager.ActivityId =
                    Guid.NewGuid();

            await base.Next.Invoke(context);
        }
    }

    internal static class AppBuilderExtensions
    {
        public static void OnDisposing(this IAppBuilder app, Action cleanup)
        {
            var properties = new AppProperties(app.Properties);
            var token = properties.OnAppDisposing;

            if (token != CancellationToken.None)
            {
                token.Register(cleanup);
            }
        }
    }
}
