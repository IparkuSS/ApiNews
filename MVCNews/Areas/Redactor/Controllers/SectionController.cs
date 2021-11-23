using Microsoft.AspNetCore.Mvc;
using MVCNews.Helper;
using MVCNews.Models;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MVCNews.Areas.Redactor.Controllers
{
    [Area("Redactor")]
    public class SectionController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        public SectionController(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }


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

        [HttpPost]
        public IActionResult CreateSection(SectionData sectionData, IFormFile titleImageFile)
        {
            /* if (ModelState.IsValid)
             {*/

            if (titleImageFile != null)
            {
                sectionData.TitleImagePath = titleImageFile.FileName;
                using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                {
                    titleImageFile.CopyTo(stream);
                }
            }


            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<SectionData>($"api/section", sectionData);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
            /* }*/

        }



        [HttpPost]
        public IActionResult PutSection(SectionData sectionData, IFormFile titleImageFile)
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
                var postTask = client.PutAsJsonAsync<SectionData>($"api/section/{sectionData.Id}", sectionData);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public async Task<IActionResult> PutSection(Guid id)
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
        public IActionResult CreateSection()
        {
            return View();
        }

        public async Task<IActionResult> DeleteSection(Guid id)
        {
            var section = new SectionData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/section/{id}");
            return RedirectToAction("Index");

        }








    }
}
