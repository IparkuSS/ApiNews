using BLLNews.DataTransferObjects.SectionsDto;
using BLLNews.Interfaces;
using Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace APINews.Controllers
{
    [Route("api/section")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class SectionController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly ISectionServices _section;
        public SectionController(ILoggerManager logger, ISectionServices section)
        {
            _logger = logger;
            _section = section;
        }
        /// <summary>
        /// Returns all sections 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSection()
        {
            try
            {
                var sectionDto = await _section.GetSections();
                return Ok(sectionDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllSection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Returns specified section by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "SectionById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSection(Guid id)
        {
            try
            {
                var sectionDto = await _section.GetSection(id);
                if (sectionDto == null)
                {
                    _logger.LogError($"Section with id: {id} doesn't exist");
                    return StatusCode(404, $"Section with id: {id} doesn't exist");
                }
                return Ok(sectionDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetSection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Creates a new section
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSection([FromBody] SectionForCreationDto section)
        {
            try
            {
                if (section == null)
                {
                    _logger.LogError("SectionForCreationDto object is null");
                    return BadRequest("SectionForCreationDto object is null");
                }
                var sectionDto = await _section.CreateSection(section);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateSection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Deletes a section
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSection(Guid id)
        {
            try
            {
                var sectionDto = await _section.DeleteSection(id);
                if (sectionDto == null)
                {
                    _logger.LogError("sectionDto object is null");
                    return BadRequest("sectionDto object is null");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(DeleteSection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Updates a section
        /// </summary>
        /// <param name="id"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSection(Guid id, [FromBody] SectionForUpdateDto section)
        {
            try
            {
                if (section == null)
                {
                    _logger.LogError("SectionForCreationDto object sent from client is null.");
                    return BadRequest("SectionForCreationDto object is null");
                }
                var sectionDto = await _section.UpdateSection(id, section);
                if (sectionDto == null)
                {
                    _logger.LogInfo($"Section with id: {id} doesn't exist.");
                    return StatusCode(404, $"Section with id: {id} doesn't exist");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateSection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
