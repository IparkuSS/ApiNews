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
    [Route("api/section/{idsection}/subsection/{idsubsection}/article")]
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

        [HttpGet]
        public async Task<IActionResult> GetAriclesForSubsection(Guid idsection, Guid idsubsection)
        {
            try
            {
                var article = await _repository.Subsection.GetSubsectionAsync(idsection, idsubsection, trackChanges: false);
                if (article == null)
                {
                    _logger.LogInfo($"Subsection with id: {idsubsection} doesn't exist in the database.");
                    return NotFound();
                }

                var ArticleFromDb = await _repository.Article.GetArticlesAsync(idsection, idsubsection, trackChanges: false);

                var articleDto = _mapper.Map<IEnumerable<ArticleDto>>(ArticleFromDb);

                return Ok(articleDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAriclesForSubsection)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateArticleForSubsection(Guid idsection, Guid idsubsection,
            [FromBody] ArticleForCreationDto article)
        {
            try
            {

                if (article == null)
                {
                    _logger.LogError("EmployeeForCreationDto object sent from client is null.");
                    return BadRequest("EmployeeForCreationDto object is null");
                }

                var subsection = await _repository.Subsection.GetSubsectionAsync(idsection, idsubsection, trackChanges: false);
                if (subsection == null)
                {
                    _logger.LogInfo($"Subsection with id: {idsubsection} doesn't exist in the database.");
                    return NotFound();
                }

                var subsectionEntity = _mapper.Map<Article>(article);
                _repository.Article.CreateArticlesForSubsection(idsection, idsubsection, subsectionEntity);
                await _repository.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateArticleForSubsection)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{id}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetArticleForSubsection(Guid idsection, Guid idsubsection, Guid id)
        {
            try
            {
                var article = await _repository.Subsection.GetSubsectionAsync(idsection, idsubsection, trackChanges: false);
                if (article == null)
                {
                    _logger.LogInfo($"Subsection with id: {id} doesn't exist in the database.");
                    return NotFound();
                }

                var articleDb = await _repository.Article.GetArticleAsync(idsubsection, id, trackChanges: false);
                if (articleDb == null)
                {
                    _logger.LogInfo($"Article with id: {id} doesn't exist in the database.");
                    return NotFound();
                }

                var articleDto = _mapper.Map<ArticleDto>(articleDb);

                return Ok(articleDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetArticleForSubsection)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }





        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubsectionForSection(Guid idsection, Guid idsubsection, Guid id)
        {
            try
            {
                var subSection = await _repository.Subsection.GetSubsectionAsync(idsection, idsubsection, trackChanges: false);
                if (subSection == null)
                {
                    _logger.LogInfo($"Subsecton with id: {idsubsection} doesn't exist in the database.");
                    return NotFound();
                }

                var ArticleForSubsection = await _repository.Article.GetArticleAsync(idsubsection, id, trackChanges: false);
                if (ArticleForSubsection == null)
                {
                    _logger.LogInfo($"Article with id: {id} doesn't exist in the database.");
                    return NotFound();
                }

                _repository.Article.DeleteArticle(ArticleForSubsection);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(DeleteSubsectionForSection)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAericleForSubsection(Guid idsection, Guid idsubsection, Guid id, [FromBody] ArticleForUpdateDto article)
        {
            try
            {
                if (article == null)
                {
                    _logger.LogError("ArticleForUpdateDto object sent from client is null.");
                    return BadRequest("ArticleForUpdateDto object is null");
                }

                var subsection = await _repository.Subsection.GetSubsectionAsync(idsection, idsubsection, trackChanges: false);
                if (subsection == null)
                {
                    _logger.LogInfo($"Subsection with id: {idsubsection} doesn't exist in the database.");
                    return NotFound();
                }

                var articleEntity = await _repository.Article.GetArticleAsync(idsubsection, id, trackChanges: true);
                if (articleEntity == null)
                {
                    _logger.LogInfo($"Article with id: {id} doesn't exist in the database.");
                    return NotFound();
                }

                _mapper.Map(article, articleEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateAericleForSubsection)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }
    }
}
