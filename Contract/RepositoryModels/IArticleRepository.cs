using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetArticlesAsync(Guid idsection, Guid idsubsection, bool trackChanges);
        Task<Article> GetArticleAsync(Guid idsubsection, Guid id, bool trackChanges);
        void CreateArticlesForSubsection(Guid sectionId, Guid subsectionId, Article employee);

        void DeleteArticle(Article article);
    }
}
