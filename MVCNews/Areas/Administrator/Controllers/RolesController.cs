using Microsoft.AspNetCore.Mvc;

namespace MVCNews.Areas.Administrator.Controllers
{
    public class RolesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
