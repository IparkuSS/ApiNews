using News.MVC.Helper.Contracts;
using News.MVC.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace News.MVC.Helper
{
    public class ClientAuthor : IClientAuthor
    {
        private readonly IApiModels _api;

        public ClientAuthor(IApiModels api)
        {
            _api = api;
        }
        public async Task<bool> CreateAuthorApi(AuthorData authorData)
        {
            HttpClient client = _api.Initial();
            var postTask = await client.PostAsJsonAsync<AuthorData>("api/author", authorData);
            if (postTask.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<bool> DeleteAuthorApi(Guid id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/author/{id}");
            if (res.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<HttpResponseMessage> GetAuthorsApi()
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/author");
            if (res.IsSuccessStatusCode)
            {
                return res;
            }
            return null;
        }

    }
}
