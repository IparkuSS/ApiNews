using Microsoft.AspNetCore.Mvc;
using News.MVC.Helper;
using News.MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
namespace News.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiModels _api;
        public HomeController(IApiModels api)
        {
            _api = api;
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
        public async Task<IActionResult> Deatails(Guid id)
        {
            var section = new SectionData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                section = JsonConvert.DeserializeObject<SectionData>(result);
            }
            return View(section);
        }
        public async Task<IActionResult> Articles(Guid sectionId, Guid subsectionId)
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
        public async Task<IActionResult> Article(Guid subsectionId, Guid id, Guid sectionId)
        {
            var article = new ArticleData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{sectionId}/subsection/{subsectionId}/article/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                article = JsonConvert.DeserializeObject<ArticleData>(result);
            }
            return View(article);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
