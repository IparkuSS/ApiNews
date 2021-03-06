using Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.BLL.DataTransferObjects.ArticlesDto;
using News.BLL.Interfaces;
using System;
using System.Threading.Tasks;
using News.DAL.Parameters;

namespace News.API.Controllers
{
    [Route("api/section/{sectionId}/subsection/{subsectionId}/article")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IArticleServices _articleService;
        public ArticleController(ILoggerManager logger, IArticleServices article)
        {
            _logger = logger;
            _articleService = article;
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
        public async Task<IActionResult> GetAriclesForSubsection(Guid sectionId, Guid subsectionId, [FromQuery] ArticlesParameters articlesParameters)
        {
            var articleDto = await _articleService.GetAriclesForSubsection(sectionId, subsectionId, articlesParameters);
            if (articleDto == null)
            {
                _logger.LogInfo($"Subsection with id: {subsectionId} doesn't exist");
                return NotFound($"Subsection with id: {subsectionId} doesn't exist");
            }
            return Ok(articleDto);
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
            if (article == null)
            {
                _logger.LogError("EmployeeForCreationDto object sent from client is null.");
                return NotFound("EmployeeForCreationDto object is null");
            }
            var articleCreateResult = await _articleService.CreateAricleForSubsection(sectionId, subsectionId, article);
            if (articleCreateResult == false)
            {
                _logger.LogInfo($"Subsection with id: {subsectionId} doesn't exist.");
                return NotFound($"Subsection with id: {subsectionId} doesn't exist.");
            }
            return NoContent();
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
            var articleDto = await _articleService.GetAricleForSubsection(sectionId, subsectionId, id);
            if (articleDto == null)
            {
                _logger.LogInfo($"Subsection with id: {subsectionId} doesn't exist");
                return NotFound($"Subsection with id: {subsectionId} doesn't exist");
            }
            return Ok(articleDto);
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
            var articleDto = await _articleService.DeleteAricleForSubsection(sectionId, subsectionId, id);
            if (articleDto == false)
            {
                _logger.LogInfo($"Article with id: {id} doesn't exist.");
                return NotFound($"Article with id: {id} doesn't exist.");
            }
            return NoContent();
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
            if (article == null)
            {
                _logger.LogError("ArticleForUpdateDto object sent from client is null.");
                return BadRequest("ArticleForUpdateDto object is null");
            }
            var articleDto = await _articleService.UpdateArticleForSubsection(sectionId, subsectionId, id, article);
            if (articleDto == false)
            {
                _logger.LogInfo($"Article with id: {id} doesn't exist.");
                return NotFound($"Article with id: {id} doesn't exist.");
            }
            return NoContent();
        }
    }
}
