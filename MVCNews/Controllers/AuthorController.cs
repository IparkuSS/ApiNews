using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.MVC.Models;
using News.MVC.Services.Contracts;
using System.Threading.Tasks;
namespace News.MVC.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorSerives _authorSerives;
        public AuthorController(IAuthorSerives authorSerives)
        {
            _authorSerives = authorSerives;
        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(AuthorData author, IFormFile PdfFile)
        {
            if (ModelState.IsValid)
            {
                var authorServicesReslt = await _authorSerives.CreateAuthor(author, PdfFile);
                if (authorServicesReslt == true)
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }
    }
}
