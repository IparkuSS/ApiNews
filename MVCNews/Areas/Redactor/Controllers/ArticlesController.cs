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
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IApiModels _api;
        public ArticlesController(IWebHostEnvironment hostingEnvironment, IApiModels api)
        {
            this.hostingEnvironment = hostingEnvironment;
            _api = api;
        }
        public async Task<IActionResult> GetArticles(Guid sectionId, Guid subsectionId)
        {
            var article = new List<ArticleData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{sectionId}/subsection/{subsectionId}/article");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                article = JsonConvert.DeserializeObject<List<ArticleData>>(result);
            }
            ViewBag.idsection = sectionId;
            ViewBag.idsubsection = subsectionId;
            return View(article);
        }
        public async Task<IActionResult> Subsections(Guid id)
        {
            var subsection = new List<SubsectionData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{id}/subsection");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                subsection = JsonConvert.DeserializeObject<List<SubsectionData>>(result);
            }
            return View(subsection);
        }
        public async Task<IActionResult> Index()
        {
            var section = new List<SectionData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/section");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                section = JsonConvert.DeserializeObject<List<SectionData>>(result);
            }
            return View(section);
        }
        [HttpPost]
        public async Task<IActionResult> CreateArticle(Guid sectionId, Guid subsectionId, ArticleData article, IFormFile titleImageFile)
        {
            HttpClient client = _api.Initial();
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    article.Image = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                article.AddTime = DateTime.Now;
                var postTask = await client.PostAsJsonAsync<ArticleData>($"api/section/{sectionId}/subsection/{subsectionId}/article", article);
                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public IActionResult CreateArticle(Guid sectionId, Guid subsectionId)
        {
            ViewBag.idsection = sectionId;
            ViewBag.idsubsection = subsectionId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PutArticle(Guid sectionId, Guid subsectionId, ArticleData article, IFormFile titleImageFile)
        {
            HttpClient client = _api.Initial();
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    article.Image = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                article.AddTime = DateTime.Now;
                var postTask = await client.PutAsJsonAsync<ArticleData>($"api/section/{sectionId}/subsection/{subsectionId}/article/{article.Id}", article);
                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public async Task<IActionResult> PutArticle(Guid id, Guid sectionId, Guid subsectionId)
        {
            var article = new ArticleData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{sectionId}/subsection/{subsectionId}/article/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                article = JsonConvert.DeserializeObject<ArticleData>(result);
            }
            ViewBag.idsection = sectionId;
            ViewBag.idsubsection = subsectionId;
            return View(article);
        }
        public async Task<IActionResult> DeleteArticle(Guid id, Guid sectionId, Guid subsectionId)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/section/{sectionId}/subsection/{subsectionId}/article/{id}");
            return RedirectToAction("Index");
        }
    }
}
