using Microsoft.AspNetCore.Mvc;

namespace AYE.Abp.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
