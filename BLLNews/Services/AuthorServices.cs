using AutoMapper;
using BLLNews.DataTransferObjects.AuthorsDto;
using BLLNews.Interfaces;
using DALNews.Models;
using DALNews.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BLLNews.Services
{
    /// <summary>
    /// the service class that serves the authors controller
    /// </summary>
    public class AuthorServices : IAuthorServices
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public AuthorServices(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> CreateAuthor(AuthorCreatDto authorCreatDto)
        {
            var authorEntity = _mapper.Map<Author>(authorCreatDto);
            _repository.Author.CreateAuthor(authorEntity);
            _repository.Author.SaveAuthor();
            return true;
        }
        public async Task<string> DeleteAuthor(Guid id)
        {
            var author = await _repository.Author.GetAuthorAsync(id, trackChanges: false);
            if (author == null)
            {
                return null;
            }
            _repository.Author.DeleteAuthor(author);
            _repository.Author.SaveAuthor();
            return "Ok";
        }
        public async Task<AuthorDto> GetAuthor(Guid id)
        {
            var author = await _repository.Author.GetAuthorAsync(id, trackChanges: false);
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
            var author = await _repository.Author.GetAllAuthorsAsync(trackChanges: false);
            var authorDto = _mapper.Map<IEnumerable<AuthorDto>>(author);
            return authorDto;
        }
    }
}
