using Contract.Repositories;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace LoggerService.RepositoryModels
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
        public async Task<IEnumerable<Author>> GetAllAuthorsAsync(bool trackChanges) =>
             await FindAll(trackChanges)
             .OrderBy(c => c.Id).ToListAsync();
        public async Task<Author> GetAuthorAsync(Guid authorId, bool trackChanges) =>
             await FindByCondition(c => c.Id.Equals(authorId), trackChanges)
             .SingleOrDefaultAsync();
        public void DeleteAuthor(Author author) => Delete(author);
        public void CreateAuthor(Author author) => Create(author);
    }
}
