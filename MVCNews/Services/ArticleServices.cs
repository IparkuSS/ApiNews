using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using News.MVC.ClientsApi.Contracts;
using News.MVC.Models;
using News.MVC.Services.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace News.MVC.Services
{
    public class ArticleServices : IArticleServices
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IClientArticle _clientArticle;
        public ArticleServices(IWebHostEnvironment hostingEnvironment, IClientArticle clientArticle)
        {
            this.hostingEnvironment = hostingEnvironment;
            _clientArticle = clientArticle;
        }
        public async Task<bool> CreateAricle(ArticleData articleData, Guid sectionId, Guid subsectionId, IFormFile titleImageFile)
        {
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
            var res = await _clientArticle.CreateArticleApi(sectionId, subsectionId, articleData);
            if (res == true) return true;
            return false;
        }
        public async Task<bool> DeleteArticle(Guid sectionId, Guid subsectionId, Guid id)
        {
            var res = await _clientArticle.DeleteArticleApi(sectionId, subsectionId, id);
            if (res == true) return true;
            return false;
        }
        public async Task<ArticleData> GetArticle(Guid sectionId, Guid subsectionId, Guid id)
        {
            var article = new ArticleData();
            var res = await _clientArticle.GetArticleApi(sectionId, subsectionId, id);
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
            var res = await _clientArticle.GetArticlesApi(sectionId, subsectionId);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                article = JsonConvert.DeserializeObject<List<ArticleData>>(result);
            }
            return article;
        }
        public async Task<bool> UpdateArticle(ArticleData articleData, Guid sectionId, Guid subsectionId, Guid id, IFormFile titleImageFile)
        {
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
            var res = await _clientArticle.UpdateArticleApi(sectionId, subsectionId, articleData, id);
            if (res == true) return true;
            return false;
        }
    }
}
