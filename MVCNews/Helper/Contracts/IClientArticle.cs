using News.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace News.MVC.Helper.Contracts
{
    public interface IClientArticle
    {
        Task<HttpResponseMessage> GetArticlesApi(Guid sectionId, Guid subsectionId);

        Task<HttpResponseMessage> GetArticleApi(Guid sectionId, Guid subsectionId, Guid id);

        Task<bool> CreateArticleApi(Guid sectionId, Guid subsectionId, ArticleData articleData);

        Task<bool> UpdateArticleApi(Guid sectionId, Guid subsectionId, ArticleData articleData, Guid id);

        Task<bool> DeleteArticleApi(Guid sectionId, Guid subsectionId, Guid id);
    }
}
