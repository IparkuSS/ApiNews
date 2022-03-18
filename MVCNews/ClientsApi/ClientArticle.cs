using News.MVC.ClientsApi.Contracts;
using News.MVC.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace News.MVC.ClientsApi
{
    public class ClientArticle : IClientArticle
    {
        private readonly IApiModels _api;

        public ClientArticle(IApiModels api)
        {
            _api = api;
        }
        public async Task<bool> CreateArticleApi(Guid sectionId, Guid subsectionId, ArticleData articleData)
        {
            HttpClient client = _api.Initial();
            var postTask = await client.PostAsJsonAsync<ArticleData>($"api/section/{sectionId}/subsection/{subsectionId}/article", articleData);
            if (postTask.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteArticleApi(Guid sectionId, Guid subsectionId, Guid id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/section/{sectionId}/subsection/{subsectionId}/article/{id}");
            if (res.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<HttpResponseMessage> GetArticleApi(Guid sectionId, Guid subsectionId, Guid id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{sectionId}/subsection/{subsectionId}/article/{id}");
            if (res.IsSuccessStatusCode)
            {
                return res;
            }
            return null;
        }

        public async Task<HttpResponseMessage> GetArticlesApi(Guid sectionId, Guid subsectionId)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{sectionId}/subsection/{subsectionId}/article");
            if (res.IsSuccessStatusCode)
            {
                return res;
            }
            return null;
        }

        public async Task<bool> UpdateArticleApi(Guid sectionId, Guid subsectionId, ArticleData articleData, Guid id)
        {
            HttpClient client = _api.Initial();
            var postTask = await client.PutAsJsonAsync<ArticleData>($"api/section/{sectionId}/subsection/{subsectionId}/article/{articleData.Id}", articleData);
            if (postTask.IsSuccessStatusCode) return true;
            return false;
        }
    }
}
