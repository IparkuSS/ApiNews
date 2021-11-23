using Microsoft.AspNetCore.Mvc;

namespace MVCNews.Areas.Redactor.Controllers
{
    [Area("Redactor")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
