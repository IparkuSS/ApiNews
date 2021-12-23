﻿using DALNews.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DALNews.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync(bool trackChanges);
        Task<Author> GetAuthorAsync(Guid authorId, bool trackChanges);
        void CreateAuthor(Author author);
        void DeleteAuthor(Author author);
        void SaveAuthor();
    }
}