using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCNews.Helper;
using MVCNews.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MVCNews.Controllers
{
    public class AuthorController : Controller
    {
        ApiModels _api = new ApiModels();
        private readonly IWebHostEnvironment hostingEnvironment;
        public AuthorController(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult create(AuthorData author, IFormFile PdfFile)
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
                var postTask = client.PostAsJsonAsync<AuthorData>("api/author", author);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return View();
                }

            }

            return View();
        }


    }
}
