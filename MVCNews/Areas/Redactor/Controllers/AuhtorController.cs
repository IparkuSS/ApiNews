using Microsoft.AspNetCore.Mvc;
using MVCNews.Helper;
using MVCNews.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;

namespace MVCNews.Areas.Redactor.Controllers
{
    [Area("Redactor")]
    public class AuhtorController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        public AuhtorController(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        ApiModels _api = new ApiModels();
        public async Task<IActionResult> Index()
        {
            List<AuthorData> apiModels = new List<AuthorData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/author");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                apiModels = JsonConvert.DeserializeObject<List<AuthorData>>(result);
            }
            return View(apiModels);
        }

        public FileResult DownloadFile(string fileName)
        {

            string path = Path.Combine(hostingEnvironment.WebRootPath, "Doc/", fileName);

            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", fileName);
        }

        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var author = new AuthorData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/author/{id}");
            return RedirectToAction("Index");

        }

    }
}
