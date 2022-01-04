using News.BLL.DataTransferObjects.AuthorsDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace News.BLL.Interfaces
{
    public interface IAuthorServices
    {
        Task<IEnumerable<AuthorDto>> GetAuthors();
        Task<AuthorDto> GetAuthor(Guid id);
        Task<bool> CreateAuthor(AuthorCreatDto authorCreatDto);
        Task<bool> DeleteAuthor(Guid id);
    }
}
