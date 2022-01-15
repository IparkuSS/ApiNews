using News.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace News.DAL.RepositoryModels.Contracts
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();

        Task<Author> GetAuthorAsync(Guid authorId);

        void CreateAuthor(Author author);

        void DeleteAuthor(Author author);

    }
}
