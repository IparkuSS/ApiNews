using News.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace News.MVC.Helper.Contracts
{
    public interface IClientSubsection
    {
        Task<HttpResponseMessage> GetSubsectionsApi(Guid sectionId);

        Task<HttpResponseMessage> GetSubsectionApi(Guid sectionId, Guid id);

        Task<bool> CreateSubsectionApi(Guid sectionId, SubsectionData subsectionData);

        Task<bool> UpdateSubsectionApi(Guid sectionId, SubsectionData subsectionData, Guid id);

        Task<bool> DeleteSubsectionApi(Guid sectionId, Guid id);
    }
}
