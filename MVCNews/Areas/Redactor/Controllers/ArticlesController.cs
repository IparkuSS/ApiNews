using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCNews.Helper;
using MVCNews.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MVCNews.Areas.Redactor.Controllers
{
    [Area("Redactor")]
    public class ArticlesController : Controller
    {
        ApiModels _api = new ApiModels();

        private readonly IWebHostEnvironment hostingEnvironment;
        public ArticlesController(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> GetArticles(Guid idsection, Guid idsubsection)
        {
            List<ArticleData> apiModels = new List<ArticleData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{idsection}/subsection/{idsubsection}/article");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                apiModels = JsonConvert.DeserializeObject<List<ArticleData>>(result);
            }

            ViewBag.idsection = idsection;
            ViewBag.idsubsection = idsubsection;
            return View(apiModels);
        }


        public async Task<IActionResult> Subsections(Guid id)
        {
            List<SubsectionData> apiModels = new List<SubsectionData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{id}/subsection");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                apiModels = JsonConvert.DeserializeObject<List<SubsectionData>>(result);
            }


            return View(apiModels);
        }

        public async Task<IActionResult> Index()
        {
            List<SectionData> apiModels = new List<SectionData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/section");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                apiModels = JsonConvert.DeserializeObject<List<SectionData>>(result);
            }


            return View(apiModels);
        }



        [HttpPost]
        public IActionResult CreateArticle(Guid sectionId, Guid subsectionId, ArticleData articleData, IFormFile titleImageFile)
        {

            HttpClient client = _api.Initial();
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    articleData.Image = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                articleData.AddTime = DateTime.Now;

                var postTask = client.PostAsJsonAsync<ArticleData>($"api/section/{sectionId}/subsection/{subsectionId}/article", articleData);
                postTask.Wait();
                var result = postTask.Result;


                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public IActionResult CreateArticle(Guid idsection, Guid idsubsection)
        {
            ViewBag.idsection = idsection;
            ViewBag.idsubsection = idsubsection;

            return View();
        }

        [HttpPost]
        public IActionResult PutArticle(Guid idsection, Guid idsubsection, ArticleData articleData, IFormFile titleImageFile)
        {

            HttpClient client = _api.Initial();
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    articleData.Image = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                articleData.AddTime = DateTime.Now;

                var postTask = client.PutAsJsonAsync<ArticleData>($"api/section/{idsection}/subsection/{idsubsection}/article/{articleData.Id}", articleData);
                postTask.Wait();
                var result = postTask.Result;


                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public async Task<IActionResult> PutArticle(Guid id, Guid idsection, Guid idsubsection)
        {
            ArticleData apiModels = new ArticleData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{idsection}/subsection/{idsubsection}/article/{id}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                apiModels = JsonConvert.DeserializeObject<ArticleData>(result);
            }

            ViewBag.idsection = idsection;
            ViewBag.idsubsection = idsubsection;

            return View(apiModels);

        }
        public async Task<IActionResult> DeleteArticle(Guid id, Guid idsubsection, Guid idsection)
        {
            var section = new ArticleData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/section/{idsection}/subsection/{idsubsection}/article/{id}");
            return RedirectToAction("Index");

        }





    }
}
