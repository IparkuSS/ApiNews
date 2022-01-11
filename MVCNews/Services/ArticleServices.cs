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
    public class ArticleServices : IArticleServices
    {
        private readonly IApiModels _api;
        private readonly IWebHostEnvironment hostingEnvironment;
        public ArticleServices(IApiModels api, IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            _api = api;
        }
        public async Task<bool> CreateAricle(ArticleData articleData, Guid sectionId, Guid subsectionId, IFormFile titleImageFile)
        {
            HttpClient client = _api.Initial();
            if (titleImageFile != null)
            {
                articleData.Image = titleImageFile.FileName;
                using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                {
                    titleImageFile.CopyTo(stream);
                }
            }
            else return false;
            articleData.AddTime = DateTime.Now;
            var postTask = await client.PostAsJsonAsync<ArticleData>($"api/section/{sectionId}/subsection/{subsectionId}/article", articleData);
            if (postTask.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<bool> DeleteArticle(Guid sectionId, Guid subsectionId, Guid id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/section/{sectionId}/subsection/{subsectionId}/article/{id}");
            if (res.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<ArticleData> GetArticle(Guid sectionId, Guid subsectionId, Guid id)
        {
            var article = new ArticleData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{sectionId}/subsection/{subsectionId}/article/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                article = JsonConvert.DeserializeObject<ArticleData>(result);
            }
            return article;
        }

        public async Task<IEnumerable<ArticleData>> GetArticles(Guid sectionId, Guid subsectionId)
        {
            var article = new List<ArticleData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/section/{sectionId}/subsection/{subsectionId}/article");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                article = JsonConvert.DeserializeObject<List<ArticleData>>(result);
            }
            return article;
        }

        public async Task<bool> UpdateArticle(ArticleData articleData, Guid sectionId, Guid subsectionId, Guid id, IFormFile titleImageFile)
        {
            HttpClient client = _api.Initial();
            if (titleImageFile != null)
            {
                articleData.Image = titleImageFile.FileName;
                using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                {
                    titleImageFile.CopyTo(stream);
                }
            }
            else return false;
            articleData.AddTime = DateTime.Now;
            var postTask = await client.PutAsJsonAsync<ArticleData>($"api/section/{sectionId}/subsection/{subsectionId}/article/{articleData.Id}", articleData);
            if (postTask.IsSuccessStatusCode) return true;
            return false;


        }
    }
}
