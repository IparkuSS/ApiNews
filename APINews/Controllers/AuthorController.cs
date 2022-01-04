using Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.BLL.DataTransferObjects.AuthorsDto;
using News.BLL.Interfaces;
using System;
using System.Threading.Tasks;
namespace APINews.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IAuthorServices _authorService;
        public AuthorController(ILoggerManager logger, IAuthorServices author)
        {
            _authorService = author;
            _logger = logger;
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
            var authorDto = await _authorService.GetAuthors();
            return Ok(authorDto);
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
            var authorDto = await _authorService.GetAuthor(id);
            if (authorDto == null)
            {
                _logger.LogInfo($"Author with id: {id} doesn't exist.");
                return NotFound($"Author with id: {id} doesn't exist.");
            }
            return Ok(authorDto);
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
            if (author == null)
            {
                _logger.LogError("AuthorCreatDto object sent from client is null.");
                return BadRequest("AuthorCreatDto object is null");
            }
            var authorDto = await _authorService.CreateAuthor(author);
            return NoContent();
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
            var authorDto = await _authorService.DeleteAuthor(id);
            if (authorDto == false)
            {
                _logger.LogInfo($"Author with id: {id} doesn't exist.");
                return NotFound($"Author with id: {id} doesn't exist.");
            }
            return NoContent();
        }
    }
}
