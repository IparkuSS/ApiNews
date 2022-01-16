using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using News.MVC.Helper.Contracts;
using News.MVC.Models;
using News.MVC.Services.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace News.MVC.Services
{
    public class SectionServices : ISectionServices
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IClientSection _clientSection;
        public SectionServices(IWebHostEnvironment hostingEnvironment, IClientSection client)
        {
            this.hostingEnvironment = hostingEnvironment;
            _clientSection = client;
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
            var res = await _clientSection.CreateSectionApi(sectionData);
            if (res == true) return true;
            return false;
        }
        public async Task<bool> DeleteSection(Guid id)
        {
            var res = await _clientSection.DeleteSectionApi(id);
            if (res == true) return true;
            return false;
        }
        public async Task<SectionData> GetSection(Guid id)
        {
            var section = new SectionData();
            var res = await _clientSection.GetSectionApi(id);
            if (res != null)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                section = JsonConvert.DeserializeObject<SectionData>(result);
            }
            return section;
        }
        public async Task<IEnumerable<SectionData>> GetSections()
        {
            var section = new List<SectionData>();
            var res = await _clientSection.GetSectionsApi();
            if (res != null)
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
            var res = await _clientSection.UpdateSectionApi(sectionData, id);
            if (res == true) return true;
            return false;
        }
    }
}
