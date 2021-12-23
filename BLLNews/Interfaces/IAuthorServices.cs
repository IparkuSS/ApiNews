using BLLNews.DataTransferObjects.AuthorsDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BLLNews.Interfaces
{
    public interface IAuthorServices
    {
        Task<IEnumerable<AuthorDto>> GetAuthors();
        Task<AuthorDto> GetAuthor(Guid id);
        Task<bool> CreateAuthor(AuthorCreatDto authorCreatDto);
        Task<string> DeleteAuthor(Guid id);
    }
}
