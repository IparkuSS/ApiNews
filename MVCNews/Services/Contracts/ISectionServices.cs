using Microsoft.AspNetCore.Http;
using News.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.MVC.Services.Contracts
{
    public interface ISectionServices
    {
        Task<IEnumerable<SectionData>> GetSections();

        Task<SectionData> GetSection(Guid id);

        Task<bool> CreateSection(SectionData sectionData, IFormFile titleImageFile);

        Task<bool> UpdateSection(SectionData sectionData, Guid id, IFormFile titleImageFile);

        Task<bool> DeleteSection(Guid id);
    }
}
