using Microsoft.EntityFrameworkCore;
using News.DAL.Models;
using News.DAL.RepositoryModels.Contracts;
using News.DAL.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.DAL.RepositoryModels
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        private readonly TrackSettings _trackSettings;
        public AuthorRepository(RepositoryContext repositoryContext, TrackSettings trackSettings) : base(repositoryContext) { _trackSettings = trackSettings; }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync() =>
             await FindAll(_trackSettings.TrackChanges)
             .OrderBy(c => c.Id).ToListAsync();

        public async Task<Author> GetAuthorAsync(Guid authorId) =>
             await FindByCondition(c => c.Id.Equals(authorId), _trackSettings.TrackChanges)
             .SingleOrDefaultAsync();

        public void DeleteAuthor(Author author) => Delete(author);

        public void CreateAuthor(Author author) => Create(author);


    }
}
