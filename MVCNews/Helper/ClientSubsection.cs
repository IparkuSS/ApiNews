using News.MVC.Helper.Contracts;
using News.MVC.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace News.MVC.Helper
{
    public class ClientSubsection : IClientSubsection
    {
        private readonly IApiModels _api;

        public ClientSubsection(IApiModels api)
        {
            _api = api;
        }
        public async Task<bool> CreateSubsectionApi(Guid sectionId, SubsectionData subsectionData)
        {
            HttpClient client = _api.Initial();
            var postTask = await client.PostAsJsonAsync<SubsectionData>($"api/section/{sectionId}/subsection", subsectionData);
            if (postTask.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteSubsectionApi(Guid sectionId, Guid id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/section/{sectionId}/subsection/{id}");
            if (res.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<HttpResponseMessage> GetSubsectionApi(Guid sectionId, Guid id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{sectionId}/subsection/{id}");
            if (res.IsSuccessStatusCode)
            {
                return res;
            }
            return null;
        }

        public async Task<HttpResponseMessage> GetSubsectionsApi(Guid sectionId)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{sectionId}/subsection");// may not work
            if (res.IsSuccessStatusCode)
            {
                return res;
            }
            return null;
        }

        public async Task<bool> UpdateSubsectionApi(Guid sectionId, SubsectionData subsectionData, Guid id)
        {
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
