using Contract;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerService.RepositoryModels
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public async Task<IEnumerable<Article>> GetArticlesAsync(Guid idsection, Guid idsubsection, bool trackChanges) =>
         await FindByCondition(e => e.IdSubsection.Equals(idsubsection), trackChanges).OrderBy(c => c.Priority).ToListAsync();



        public async Task<Article> GetArticleAsync(Guid idsubsection, Guid id, bool trackChanges) =>
           await FindByCondition(e => e.IdSubsection.Equals(idsubsection) && e.Id.Equals(id), trackChanges)
               .SingleOrDefaultAsync();


        public void CreateArticlesForSubsection(Guid sectionId, Guid subsectionId, Article employee)
        {
            employee.IdSubsection = subsectionId; Create(employee);
        }

        public void DeleteArticle(Article article) { Delete(article); }


    }
}
