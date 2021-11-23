using AutoMapper;
using Contract;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APINews.Controllers
{
    [Route("api/author")]
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

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            try
            {
                var author = await _repository.Author.GetAllAuthorsAsync(trackChanges: false);

                var authorDto = _mapper.Map<IEnumerable<Author>>(author);
                return Ok(authorDto);



            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAuthors)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}", Name = "AuthorById")]
        public async Task<IActionResult> GetAuthor(Guid id)
        {

            try
            {
                var author = await _repository.Author.GetAuthorAsync(id, trackChanges: false);
                if (author == null)
                {
                    _logger.LogInfo($"Author with id: {id} doesn't exist in the database.");
                    return NotFound();
                }
                else
                {
                    var authorDto = _mapper.Map<SectionDto>(author);
                    return Ok(authorDto);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAuthor)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
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


                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAuthor)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            try
            {
                var author = await _repository.Author.GetAuthorAsync(id, trackChanges: false);
                if (author == null)
                {
                    _logger.LogInfo($"Author with id: {id} doesn't exist in the database.");
                    return NotFound();
                }

                _repository.Author.DeleteAuthor(author);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(DeleteAuthor)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }
    }
}
