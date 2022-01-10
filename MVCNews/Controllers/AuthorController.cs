using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.MVC.Helper;
using News.MVC.Models;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace News.MVC.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IApiModels _api;
        public AuthorController(IWebHostEnvironment hostingEnvironment, IApiModels api)
        {
            this.hostingEnvironment = hostingEnvironment;
            _api = api;
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
                if (PdfFile != null)
                {
                    author.Document = PdfFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "Doc/", PdfFile.FileName), FileMode.Create))
                    {
                        PdfFile.CopyTo(stream);
                    }
                }
                HttpClient client = _api.Initial();
                var postTask = await client.PostAsJsonAsync<AuthorData>("api/author", author);
                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
            }
            return View();
        }
    }
}
