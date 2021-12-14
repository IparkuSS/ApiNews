using AutoMapper;
using Contract;
using Contract.Repositories;
using Entities.DataTransferObjects.AuthorsDto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace APINews.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public AuthorController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Returns all authors 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthors()
        {
            try
            {
                var author = await _repository.Author.GetAllAuthorsAsync(trackChanges: false);
                var authorDto = _mapper.Map<IEnumerable<AuthorDto>>(author);
                return Ok(authorDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAuthors)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Returns specified author by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "AuthorById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthor(Guid id)
        {
            try
            {
                var author = await _repository.Author.GetAuthorAsync(id, trackChanges: false);
                if (author == null)
                {
                    _logger.LogInfo($"Author with id: {id} doesn't exist.");
                    return StatusCode(404, $"Author with id: {id} doesn't exist");
                }
                else
                {
                    var authorDto = _mapper.Map<AuthorDto>(author);
                    return Ok(authorDto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAuthor)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Creates a new author
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreatDto author)
        {
            try
            {
                if (author == null)
                {
                    _logger.LogError("AuthorCreatDto object sent from client is null.");
                    return BadRequest("AuthorCreatDto object is null");
                }
                var authorEntity = _mapper.Map<Author>(author);
                _repository.Author.CreateAuthor(authorEntity);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAuthor)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Deletes a author
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            try
            {
                var author = await _repository.Author.GetAuthorAsync(id, trackChanges: false);
                if (author == null)
                {
                    _logger.LogInfo($"Author with id: {id} doesn't exist.");
                    return StatusCode(404, $"Author with id: {id} doesn't exist.");
                }
                _repository.Author.DeleteAuthor(author);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(DeleteAuthor)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
