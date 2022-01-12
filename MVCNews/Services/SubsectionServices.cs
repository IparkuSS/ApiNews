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
    public class SubsectionServices : ISubsectionServices
    {
        private readonly IClientSubsection _clientSubsection;
        private readonly IWebHostEnvironment hostingEnvironment;
        public SubsectionServices(IWebHostEnvironment hostingEnvironment, IClientSubsection clientSubsection)
        {
            this.hostingEnvironment = hostingEnvironment;
            _clientSubsection = clientSubsection;
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
            var res = await _clientSubsection.CreateSubsectionApi(sectionId, subsectionData);
            if (res == true)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteSubsection(Guid id, Guid sectionId)
        {
            var res = await _clientSubsection.DeleteSubsectionApi(sectionId, id);
            if (res == true)
                return true;
            return false;
        }
        public async Task<SubsectionData> GetSubsection(Guid id, Guid sectionId)
        {
            var subsection = new SubsectionData();
            var res = await _clientSubsection.GetSubsectionApi(sectionId, id);
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
            var res = await _clientSubsection.GetSubsectionsApi(sectionId);
            if (res != null)
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
            var res = await _clientSubsection.UpdateSubsectionApi(sectionId, subsectionData, id);
            if (res == true)
            {
                return true;
            }
            return false;
        }
    }
}
