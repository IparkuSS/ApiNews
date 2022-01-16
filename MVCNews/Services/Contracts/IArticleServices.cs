using Microsoft.AspNetCore.Http;
using News.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.MVC.Services.Contracts
{
    public interface IArticleServices
    {
        Task<IEnumerable<ArticleData>> GetArticles(Guid sectionId, Guid subsectionId);

        Task<ArticleData> GetArticle(Guid sectionId, Guid subsectionId, Guid id);

        Task<bool> CreateAricle(ArticleData articleData, Guid sectionId, Guid subsectionId, IFormFile titleImageFile);

        Task<bool> UpdateArticle(ArticleData articleData, Guid sectionId, Guid subsectionId, Guid id, IFormFile titleImageFile);

        Task<bool> DeleteArticle(Guid sectionId, Guid subsectionId, Guid id);
    }
}
