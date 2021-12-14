using AutoMapper;
using Contract;
using Contract.Repositories;
using Entities.DataTransferObjects.ArticlesDto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace APINews.Controllers
{
    [Route("api/section/{sectionId}/subsection/{subsectionId}/article")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public ArticleController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Returns all articles for subsection
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="subsectionId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAriclesForSubsection(Guid sectionId, Guid subsectionId)
        {
            try
            {
                var article = await _repository.Subsection.GetSubsectionAsync(sectionId, subsectionId, trackChanges: false);
                if (article == null)
                {
                    _logger.LogInfo($"Subsection with id: {subsectionId} doesn't exist");
                    return StatusCode(404, $"Subsection with id: {subsectionId} doesn't exist");
                }
                var articleFromDb = await _repository.Article.GetArticlesAsync(sectionId, subsectionId, trackChanges: false);
                var articleDto = _mapper.Map<IEnumerable<ArticleDto>>(articleFromDb);
                return Ok(articleDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAriclesForSubsection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Creates a new articles for subsection
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="subsectionId"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateArticleForSubsection(Guid sectionId, Guid subsectionId,
            [FromBody] ArticleForCreationDto article)
        {
            try
            {
                if (article == null)
                {
                    _logger.LogError("EmployeeForCreationDto object sent from client is null.");
                    return StatusCode(400, "EmployeeForCreationDto object is null");
                }
                var subsection = await _repository.Subsection.GetSubsectionAsync(sectionId, subsectionId, trackChanges: false);
                if (subsection == null)
                {
                    _logger.LogInfo($"Subsection with id: {subsectionId} doesn't exist.");
                    return StatusCode(404, $"Subsection with id: {subsectionId} doesn't exist.");
                }
                var subsectionEntity = _mapper.Map<Article>(article);
                _repository.Article.CreateArticlesForSubsection(sectionId, subsectionId, subsectionEntity);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateArticleForSubsection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Returns specified articles by id for subsection
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="subsectionId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetEmployeeForCompany")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArticleForSubsection(Guid sectionId, Guid subsectionId, Guid id)
        {
            try
            {
                var article = await _repository.Subsection.GetSubsectionAsync(sectionId, subsectionId, trackChanges: false);
                if (article == null)
                {
                    _logger.LogInfo($"Subsection with id: {subsectionId} doesn't exist.");
                    return StatusCode(404, $"Subsection with id: {subsectionId} doesn't.");
                }
                var articleDb = await _repository.Article.GetArticleAsync(subsectionId, id, trackChanges: false);
                if (articleDb == null)
                {
                    _logger.LogInfo($"Article with id: {id} doesn't exist.");
                    return StatusCode(404, $"Article with id: {id} doesn't exist.");
                }
                var articleDto = _mapper.Map<ArticleDto>(articleDb);
                return Ok(articleDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetArticleForSubsection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Deletes a articles from a subsections
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="subsectionId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteArticleForSubsection(Guid sectionId, Guid subsectionId, Guid id)
        {
            try
            {
                var subSection = await _repository.Subsection.GetSubsectionAsync(sectionId, subsectionId, trackChanges: false);
                if (subSection == null)
                {
                    _logger.LogInfo($"Subsecton with id: {subsectionId} doesn't exist.");
                    return StatusCode(404, $"Subsecton with id: {subsectionId} doesn't exist.");
                }
                var articleForSubsection = await _repository.Article.GetArticleAsync(subsectionId, id, trackChanges: false);
                if (articleForSubsection == null)
                {
                    _logger.LogInfo($"Article with id: {id} doesn't exist.");
                    return StatusCode(404, $"Article with id: {id} doesn't exist.");
                }
                _repository.Article.DeleteArticle(articleForSubsection);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(DeleteArticleForSubsection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Updates a articles for subsections
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="subsectionId"></param>
        /// <param name="id"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateArticleForSubsection(Guid sectionId, Guid subsectionId, Guid id, [FromBody] ArticleForUpdateDto article)
        {
            try
            {
                if (article == null)
                {
                    _logger.LogError("ArticleForUpdateDto object sent from client is null.");
                    return BadRequest("ArticleForUpdateDto object is null");
                }
                var subsection = await _repository.Subsection.GetSubsectionAsync(sectionId, subsectionId, trackChanges: false);
                if (subsection == null)
                {
                    _logger.LogInfo($"Subsection with id: {subsectionId} doesn't exist.");
                    return StatusCode(404, $"Subsection with id: {subsectionId} doesn't exist.");
                }
                var articleEntity = await _repository.Article.GetArticleAsync(subsectionId, id, trackChanges: true);
                if (articleEntity == null)
                {
                    _logger.LogInfo($"Article with id: {id} doesn't exist.");
                    return StatusCode(404, $"Article with id: {id} doesn't exist.");
                }
                _mapper.Map(article, articleEntity);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateArticleForSubsection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
