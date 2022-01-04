using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVCNews.Helper;
using MVCNews.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
namespace MVCNews.Areas.Redactor.Controllers
{
    [Area("Redactor")]
    public class AuhtorController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IApiModels _api;
        public AuhtorController(IWebHostEnvironment hostingEnvironment, IApiModels api)
        {
            this.hostingEnvironment = hostingEnvironment;
            _api = api;
        }
        public async Task<IActionResult> Index()
        {
            var author = new List<AuthorData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/author");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<List<AuthorData>>(result);
            }
            return View(author);
        }
        public FileResult DownloadFile(string fileName)
        {
            string path = Path.Combine(hostingEnvironment.WebRootPath, "Doc/", fileName);
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/octet-stream", fileName);
        }
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/author/{id}");
            return RedirectToAction("Index");
        }
    }
}
