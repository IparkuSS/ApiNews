using Microsoft.AspNetCore.Mvc;
namespace News.MVC.Areas.Administrator.Controllers
{
    public class RolesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
