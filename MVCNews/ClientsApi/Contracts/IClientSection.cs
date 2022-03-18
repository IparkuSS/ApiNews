using News.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace News.MVC.ClientsApi.Contracts
{
    public interface IClientSection
    {
        Task<HttpResponseMessage> GetSectionsApi();

        Task<HttpResponseMessage> GetSectionApi(Guid id);

        Task<bool> CreateSectionApi(SectionData sectionData);

        Task<bool> UpdateSectionApi(SectionData sectionData, Guid id);

        Task<bool> DeleteSectionApi(Guid id);

    }
}
