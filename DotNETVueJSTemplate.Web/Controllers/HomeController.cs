using System.Web.Mvc;

namespace DotNETVueJSTemplate.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View("Error");
        }
    }
}