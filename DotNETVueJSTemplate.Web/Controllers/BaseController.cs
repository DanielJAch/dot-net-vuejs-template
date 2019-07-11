using System.Web.Mvc;

namespace DotNETVueJSTemplate.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Assets = WebpackHelper.GetAssets();

            base.OnActionExecuting(filterContext);
        }
    }
}