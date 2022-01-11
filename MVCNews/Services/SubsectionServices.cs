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
    public class SubsectionServices : ISubsectionServices
    {
        private readonly IApiModels _api;
        private readonly IWebHostEnvironment hostingEnvironment;
        public SubsectionServices(IApiModels api, IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            _api = api;
        }

        public async Task<bool> CreateSubsection(SubsectionData subsectionData, Guid sectionId, IFormFile titleImageFile)
        {
            if (titleImageFile != null)
            {
                subsectionData.ImagePath = titleImageFile.FileName;
                using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                {
                    titleImageFile.CopyTo(stream);
                }
            }
            else return false;
            HttpClient client = _api.Initial();
            var postTask = await client.PostAsJsonAsync<SubsectionData>($"api/section/{sectionId}/subsection", subsectionData);
            if (postTask.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteSubsection(Guid id, Guid sectionId)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/section/{sectionId}/subsection/{id}");
            if (res.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<SubsectionData> GetSubsection(Guid id, Guid sectionId)
        {
            var subsection = new SubsectionData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{sectionId}/subsection/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                subsection = JsonConvert.DeserializeObject<SubsectionData>(result);
            }
            return subsection;
        }

        public async Task<IEnumerable<SubsectionData>> GetSubsections(Guid sectionId)
        {
            var subsection = new List<SubsectionData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{sectionId}/subsection");// may not work
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                subsection = JsonConvert.DeserializeObject<List<SubsectionData>>(result);
            }
            return subsection;
        }

        public async Task<bool> UpdateSubsection(SubsectionData subsectionData, Guid id, Guid sectionId, IFormFile titleImageFile)
        {
            if (titleImageFile != null)
            {
                subsectionData.ImagePath = titleImageFile.FileName;
                using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                {
                    titleImageFile.CopyTo(stream);
                }
            }
            else return false;
            HttpClient client = _api.Initial();
            var postTask = await client.PutAsJsonAsync<SubsectionData>($"api/section/{sectionId}/subsection/{subsectionData.Id}", subsectionData);
            if (postTask.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
