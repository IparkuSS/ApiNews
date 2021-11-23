using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCNews.Helper;
using MVCNews.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCNews.Controllers
{
    public class HomeController : Controller
    {
        ApiModels _api = new ApiModels();

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
        public async Task<IActionResult> Deatails(Guid id)
        {
            var apiModels = new SectionData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{id}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                apiModels = JsonConvert.DeserializeObject<SectionData>(result);
            }

            return View(apiModels);
        }


        public async Task<IActionResult> Articles(Guid idsection, Guid idsubsection)
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
        public async Task<IActionResult> DeatailsForSubsection(Guid idSection, Guid id)
        {
            SubsectionData apiModels = new SubsectionData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{idSection}/subsection/{id}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                apiModels = JsonConvert.DeserializeObject<SubsectionData>(result);
            }


            return View(apiModels);
        }
        public async Task<IActionResult> Article(Guid idsubsection, Guid id, Guid idsection)
        {
            ArticleData apiModels = new ArticleData();
            HttpClient client = _api.Initial();


            HttpResponseMessage res = await client.GetAsync($"api/section/{idsection}/subsection/{idsubsection}/article/{id}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                apiModels = JsonConvert.DeserializeObject<ArticleData>(result);
            }


            return View(apiModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
