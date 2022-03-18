using News.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using News.DAL.Parameters;

namespace News.DAL.RepositoryModels.Contracts
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetArticlesAsync(Guid sectionid, Guid subsectionId, ArticlesParameters articlesParameters);

        Task<Article> GetArticleAsync(Guid subsectionId, Guid id);

        void CreateArticlesForSubsection(Guid sectionid, Guid subsectionId, Article article);

        void DeleteArticle(Article article);

        void SaveArticle();
    }
}
