using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using News.MVC.ClientsApi;
using News.MVC.Services.Contracts;
using System;
using System.IO;
using System.Threading.Tasks;
namespace News.MVC.Areas.Redactor.Controllers
{
    [Area("Redactor")]
    public class AuhtorController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IAuthorSerives _authorServices;
        public AuhtorController(IWebHostEnvironment hostingEnvironment, IAuthorSerives authorServices)
        {
            this.hostingEnvironment = hostingEnvironment;

            _authorServices = authorServices;
        }
        public async Task<IActionResult> Index()
        {
            var authors = await _authorServices.GetAuthors();
            return View(authors);
        }
        public FileResult DownloadFile(string fileName)
        {
            string path = Path.Combine(hostingEnvironment.WebRootPath, "Doc/", fileName);
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/octet-stream", fileName);
        }
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var authorServicesResult = await _authorServices.DeleteAuthor(id);
            if (authorServicesResult == true) return RedirectToAction("Index");
            return RedirectToAction("Index");
        }
    }
}
