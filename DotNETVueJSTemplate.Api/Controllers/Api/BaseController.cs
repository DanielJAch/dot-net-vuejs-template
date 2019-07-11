using System;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using NLog;

namespace DotNETVueJSTemplate.Api.Controllers.Api
{
    public class BaseApiController : ApiController
    {
        public BaseApiController(ILogger logger)
        {
            this.Logger = logger;
        }

        protected ILogger Logger { get; set; }

        protected IHttpActionResult HandleException(Exception e)
        {
            if (e is UnauthorizedAccessException)
            {
                return this.Content(HttpStatusCode.Unauthorized,
                    new { message = e.Message ?? "Authorization has been denied for this request." });
            }

            if (e is ObjectNotFoundException)
            {
                return this.Content(HttpStatusCode.NotFound, new { message = e.Message });
            }

            if (e is DbEntityValidationException entityValidationException)
            {
                var sb = new StringBuilder();

                foreach (var error in entityValidationException.EntityValidationErrors)
                {
                    sb.Append($"{error.Entry.Entity}{Environment.NewLine}" +
                              $"{error.ValidationErrors.Select(m => $"\t{m.PropertyName}: {m.ErrorMessage}{Environment.NewLine}")}" +
                              $"{error.Entry.CurrentValues.PropertyNames.Select(m => $"\t\t{m}: {error.Entry.CurrentValues.GetValue<string>(m)}")}" +
                              Environment.NewLine);
                }

                this.Logger.Debug(sb.ToString());
            }

            this.Logger.Error(e);

            return this.InternalServerError(e);
        }
    }
}
