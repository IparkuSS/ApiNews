using BLLNews.DataTransferObjects.ArticlesDto;
using BLLNews.Interfaces;
using Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace APINews.Controllers
{
    [Route("api/section/{sectionId}/subsection/{subsectionId}/article")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IArticleServices _article;
        public ArticleController(ILoggerManager logger, IArticleServices article)
        {
            _logger = logger;
            _article = article;
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
                var articleDto = await _article.GetAriclesForSubsection(sectionId, subsectionId);
                if (articleDto == null)
                {
                    _logger.LogInfo($"Subsection with id: {subsectionId} doesn't exist");
                    return StatusCode(404, $"Subsection with id: {subsectionId} doesn't exist");
                }
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
                var articleCreateResult = await _article.CreateAricleForSubsection(sectionId, subsectionId, article);
                if (articleCreateResult == null)
                {
                    _logger.LogInfo($"Subsection with id: {subsectionId} doesn't exist.");
                    return StatusCode(404, $"Subsection with id: {subsectionId} doesn't exist.");
                }
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
                var articleDto = await _article.GetAricleForSubsection(sectionId, subsectionId, id);
                if (articleDto == null)
                {
                    _logger.LogInfo($"Subsection with id: {subsectionId} doesn't exist");
                    return StatusCode(404, $"Subsection with id: {subsectionId} doesn't exist");
                }
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
                var articleDto = await _article.DeleteAricleForSubsection(sectionId, subsectionId, id);
                if (articleDto == null)
                {
                    _logger.LogInfo($"Article with id: {id} doesn't exist.");
                    return StatusCode(404, $"Article with id: {id} doesn't exist.");
                }
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
                var articleDto = await _article.UpdateArticleForSubsection(sectionId, subsectionId, id, article);
                if (articleDto == null)
                {
                    _logger.LogInfo($"Article with id: {id} doesn't exist.");
                    return StatusCode(404, $"Article with id: {id} doesn't exist.");
                }
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
