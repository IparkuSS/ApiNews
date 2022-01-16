using AutoMapper;
using News.BLL.DataTransferObjects.AuthorsDto;
using News.BLL.Interfaces;
using News.DAL.Models;
using News.DAL.RepositoryModels.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace News.BLL.Services
{
    /// <summary>
    /// the service class that serves the authors controller
    /// </summary>
    public class AuthorServices : IAuthorServices
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public AuthorServices(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateAuthor(AuthorCreatDto authorCreatDto)
        {
            var authorEntity = _mapper.Map<Author>(authorCreatDto);
            _authorRepository.CreateAuthor(authorEntity);
            return true;
        }
        public async Task<bool> DeleteAuthor(Guid id)
        {
            var author = await _authorRepository.GetAuthorAsync(id);
            if (author == null)
            {
                return false;
            }
            _authorRepository.DeleteAuthor(author);
            return true;
        }
        public async Task<AuthorDto> GetAuthor(Guid id)
        {
            var author = await _authorRepository.GetAuthorAsync(id);
            if (author == null)
            {
                return null;
            }
            else
            {
                var authorDto = _mapper.Map<AuthorDto>(author);
                return authorDto;
            }
        }
        public async Task<IEnumerable<AuthorDto>> GetAuthors()
        {
            var author = await _authorRepository.GetAllAuthorsAsync();
            var authorDto = _mapper.Map<IEnumerable<AuthorDto>>(author);
            return authorDto;
        }
    }
}
