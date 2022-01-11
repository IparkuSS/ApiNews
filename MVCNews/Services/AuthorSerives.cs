using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using News.MVC.Helper;
using News.MVC.Models;
using News.MVC.Services.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace News.MVC.Services
{
    public class AuthorSerives : IAuthorSerives
    {
        private readonly IApiModels _api;
        private readonly IWebHostEnvironment hostingEnvironment;
        public AuthorSerives(IApiModels api, IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            _api = api;
        }
        public async Task<bool> CreateAuthor(AuthorData authorData, IFormFile PdfFile)
        {
            if (PdfFile != null)
            {
                authorData.Document = PdfFile.FileName;
                using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "Doc/", PdfFile.FileName), FileMode.Create))
                {
                    PdfFile.CopyTo(stream);
                }
            }
            else return false;
            HttpClient client = _api.Initial();
            var postTask = await client.PostAsJsonAsync<AuthorData>("api/author", authorData);
            if (postTask.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<bool> DeleteAuthor(Guid id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/author/{id}");
            if (res.IsSuccessStatusCode) return true;
            return false;
        }

        public Task<AuthorData> GetAuthor(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AuthorData>> GetAuthors()
        {
            var author = new List<AuthorData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/author");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<List<AuthorData>>(result);
            }
            return author;
        }
    }
}
