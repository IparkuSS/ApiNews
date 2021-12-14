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
    public class SubsectionController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IApiModels _api;
        public SubsectionController(IWebHostEnvironment hostingEnvironment, IApiModels api)
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
        public async Task<IActionResult> GetSubsections(Guid id)
        {
            var subsection = new List<SubsectionData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{id}/subsection");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                subsection = JsonConvert.DeserializeObject<List<SubsectionData>>(result);
            }
            ViewBag.sectionId = id;
            return View(subsection);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubsection(Guid Id, SubsectionData subsection, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    subsection.ImagePath = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                HttpClient client = _api.Initial();
                var postTask = await client.PostAsJsonAsync<SubsectionData>($"api/section/{Id}/subsection", subsection);
                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public IActionResult CreateSubsection()
        {
            return View();
        }
        public async Task<IActionResult> PutSubsection(Guid id, Guid sectionId)
        {
            var subsection = new SubsectionData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{sectionId}/subsection/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                subsection = JsonConvert.DeserializeObject<SubsectionData>(result);
            }
            ViewBag.idsection = sectionId;
            return View(subsection);
        }
        [HttpPost]
        public async Task<IActionResult> PutSubsection(Guid idsection, SubsectionData sectionData, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    sectionData.ImagePath = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                HttpClient client = _api.Initial();
                var postTask = await client.PutAsJsonAsync<SubsectionData>($"api/section/{idsection}/subsection/{sectionData.Id}", sectionData);
                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public async Task<IActionResult> DeleteSubsection(Guid id, Guid idsection)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/section/{idsection}/subsection/{id}");
            return RedirectToAction("Index");
        }
    }
}
