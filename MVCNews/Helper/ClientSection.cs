using News.MVC.Helper.Contracts;
using News.MVC.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace News.MVC.Helper
{
    public class ClientSection : IClientSection
    {
        private readonly IApiModels _api;

        public ClientSection(IApiModels api)
        {
            _api = api;
        }

        public async Task<bool> CreateSectionApi(SectionData sectionData)
        {
            HttpClient client = _api.Initial();
            var postTask = await client.PostAsJsonAsync<SectionData>($"api/section", sectionData);
            if (postTask.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteSectionApi(Guid id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/section/{id}");
            if (res.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<HttpResponseMessage> GetSectionApi(Guid id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{id}");
            if (res.IsSuccessStatusCode)
            {
                return res;
            }
            return null;
        }

        public async Task<HttpResponseMessage> GetSectionsApi()
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/section");
            if (res.IsSuccessStatusCode)
            {
                return res;
            }
            return null;
        }

        public async Task<bool> UpdateSectionApi(SectionData sectionData, Guid id)
        {
            HttpClient client = _api.Initial();
            var postTask = await client.PutAsJsonAsync<SectionData>($"api/section/{id}", sectionData);
            if (postTask.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
