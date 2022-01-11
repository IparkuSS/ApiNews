using Microsoft.AspNetCore.Http;
using News.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.MVC.Services.Contracts
{
    public interface ISubsectionServices
    {
        Task<IEnumerable<SubsectionData>> GetSubsections(Guid sectionId);

        Task<SubsectionData> GetSubsection(Guid id, Guid sectionId);

        Task<bool> CreateSubsection(SubsectionData subsectionData, Guid sectionId, IFormFile titleImageFile);

        Task<bool> UpdateSubsection(SubsectionData subsectionData, Guid id, Guid sectionId, IFormFile titleImageFile);

        Task<bool> DeleteSubsection(Guid id, Guid sectionId);
    }
}
