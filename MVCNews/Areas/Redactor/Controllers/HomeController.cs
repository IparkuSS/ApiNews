using Microsoft.AspNetCore.Mvc;
namespace News.MVC.Areas.Redactor.Controllers
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
