using Microsoft.AspNetCore.Http;
using News.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.MVC.Services.Contracts
{
    public interface IAuthorSerives
    {
        Task<IEnumerable<AuthorData>> GetAuthors();

        Task<bool> CreateAuthor(AuthorData authorData, IFormFile PdfFile);

        Task<bool> DeleteAuthor(Guid id);
    }
}
