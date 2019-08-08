using System.Web.Mvc;

namespace DotNETVueJSTemplate.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return new FilePathResult("~/dist/index.html", "text/html");
        }

        public ActionResult Error()
        {
            return View("Error");
        }
    }
}