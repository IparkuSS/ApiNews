using Microsoft.EntityFrameworkCore;
using News.DAL.Models;
using News.DAL.RepositoryModels.Contracts;
using News.DAL.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News.DAL.Parameters;

namespace News.DAL.RepositoryModels
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        private readonly TrackSettings _trackSettings;
        public ArticleRepository(RepositoryContext repositoryContext, TrackSettings trackSettings) : base(repositoryContext) { _trackSettings = trackSettings; }

        public async Task<IEnumerable<Article>> GetArticlesAsync(Guid sectionId, Guid subsectionId,
            ArticlesParameters articlesParameters) =>
            await FindByCondition(e => e.IdSubsection.Equals(subsectionId), _trackSettings.TrackChanges)
                .OrderBy(c => c.Priority)
                .Skip((articlesParameters.PageNumber - 1) * articlesParameters.PageSize)
                .Take(articlesParameters.PageSize).ToListAsync();

        public async Task<Article> GetArticleAsync(Guid subsectionId, Guid id) =>
                await FindByCondition(e => e.IdSubsection.Equals(subsectionId) && e.Id.Equals(id), _trackSettings.TrackChanges)
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
