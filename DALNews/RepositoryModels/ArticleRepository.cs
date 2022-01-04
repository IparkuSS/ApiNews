using News.DAL.Repositories;
using News.DAL;
using News.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace News.DAL.RepositoryModels
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Article>> GetArticlesAsync(Guid sectionId, Guid subsectionId, bool trackChanges) =>
              await FindByCondition(e => e.IdSubsection.Equals(subsectionId), trackChanges).OrderBy(c => c.Priority)
              .ToListAsync();
        public async Task<Article> GetArticleAsync(Guid subsectionId, Guid id, bool trackChanges) =>
                await FindByCondition(e => e.IdSubsection.Equals(subsectionId) && e.Id.Equals(id), trackChanges)
               .SingleOrDefaultAsync();
        public void CreateArticlesForSubsection(Guid sectionId, Guid subsectionId, Article article)
        {
            article.IdSubsection = subsectionId;
            Create(article);
        }
        public void DeleteArticle(Article article) => Delete(article);

        public void SaveArticle() => Save();


    }
}
