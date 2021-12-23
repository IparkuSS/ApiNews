using DALNews.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DALNews.Repositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetArticlesAsync(Guid sectionid, Guid subsectionId, bool trackChanges);
        Task<Article> GetArticleAsync(Guid subsectionId, Guid id, bool trackChanges);
        void CreateArticlesForSubsection(Guid sectionid, Guid subsectionId, Article article);
        void DeleteArticle(Article article);
        void SaveArticle();
    }
}
