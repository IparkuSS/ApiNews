using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using News.MVC.Helper;
using News.MVC.Models;
using News.MVC.Services.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace News.MVC.Services
{
    public class SectionServices : ISectionServices
    {
        private readonly IApiModels _api;
        private readonly IWebHostEnvironment hostingEnvironment;
        public SectionServices(IApiModels api, IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            _api = api;
        }
        public async Task<bool> CreateSection(SectionData sectionData, IFormFile titleImageFile)
        {
            if (titleImageFile != null)
            {
                sectionData.TitleImagePath = titleImageFile.FileName;
                using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                {
                    titleImageFile.CopyTo(stream);
                }
            }
            else return false;
            HttpClient client = _api.Initial();
            var postTask = await client.PostAsJsonAsync<SectionData>($"api/section", sectionData);
            if (postTask.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteSection(Guid id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/section/{id}");
            if (res.IsSuccessStatusCode) return true;
            return false;
        }
        public async Task<SectionData> GetSection(Guid id)
        {
            var section = new SectionData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                section = JsonConvert.DeserializeObject<SectionData>(result);
            }
            return section;
        }
        public async Task<IEnumerable<SectionData>> GetSections()
        {
            var section = new List<SectionData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/section");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                section = JsonConvert.DeserializeObject<List<SectionData>>(result);
            }
            return section;
        }
        public async Task<bool> UpdateSection(SectionData sectionData, Guid id, IFormFile titleImageFile)
        {
            if (titleImageFile != null)
            {
                sectionData.TitleImagePath = titleImageFile.FileName;
                using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                {
                    titleImageFile.CopyTo(stream);
                }
            }
            else return false;
            HttpClient client = _api.Initial();
            var postTask = await client.PutAsJsonAsync<SectionData>($"api/section/{sectionData.Id}", sectionData);
            if (postTask.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
