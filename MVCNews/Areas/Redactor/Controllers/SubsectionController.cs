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
        ApiModels _api = new ApiModels();
        private readonly IWebHostEnvironment hostingEnvironment;
        public SubsectionController(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
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
        public async Task<IActionResult> GetSubsections(Guid id)
        {
            List<SubsectionData> apiModels = new List<SubsectionData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{id}/subsection");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                apiModels = JsonConvert.DeserializeObject<List<SubsectionData>>(result);
            }

            ViewBag.sectionId = id;
            return View(apiModels);
        }
        [HttpPost]
        public IActionResult CreateSubsection(Guid Id, SubsectionData sectionData, IFormFile titleImageFile)
        {
            /* if (ModelState.IsValid)
             {*/
            if (titleImageFile != null)
            {
                sectionData.ImagePath = titleImageFile.FileName;
                using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                {
                    titleImageFile.CopyTo(stream);
                }
            }
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<SubsectionData>($"api/section/{Id}/subsection", sectionData);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            /*}*/
            return View();
        }

        public IActionResult CreateSubsection()
        {
            return View();
        }
        public async Task<IActionResult> PutSubsection(Guid id, Guid sectionId)
        {

            SubsectionData apiModels = new SubsectionData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{sectionId}/subsection/{id}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                apiModels = JsonConvert.DeserializeObject<SubsectionData>(result);
            }

            ViewBag.idsection = sectionId;
            return View(apiModels);
        }
        [HttpPost]
        public IActionResult PutSubsection(Guid idsection, SubsectionData sectionData, IFormFile titleImageFile)
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
                var postTask = client.PutAsJsonAsync<SubsectionData>($"api/section/{idsection}/subsection/{sectionData.Id}", sectionData);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public async Task<IActionResult> DeleteSubsection(Guid id, Guid idsection)
        {
            var section = new SubsectionData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/section/{idsection}/subsection/{id}");
            return RedirectToAction("Index");

        }


    }
}
