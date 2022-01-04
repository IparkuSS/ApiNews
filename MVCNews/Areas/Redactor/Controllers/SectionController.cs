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
    public class SectionController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IApiModels _api;
        public SectionController(IWebHostEnvironment hostingEnvironment, IApiModels api)
        {
            this.hostingEnvironment = hostingEnvironment;
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
        [HttpPost]
        public async Task<IActionResult> CreateSection(SectionData section, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    section.TitleImagePath = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                HttpClient client = _api.Initial();
                var postTask = await client.PostAsJsonAsync<SectionData>($"api/section", section);
                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PutSection(SectionData sectionData, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    sectionData.TitleImagePath = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                HttpClient client = _api.Initial();
                var postTask = await client.PutAsJsonAsync<SectionData>($"api/section/{sectionData.Id}", sectionData);
                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public async Task<IActionResult> PutSection(Guid id)
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
        public IActionResult CreateSection()
        {
            return View();
        }
        public async Task<IActionResult> DeleteSection(Guid id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/section/{id}");
            return RedirectToAction("Index");
        }
    }
}
