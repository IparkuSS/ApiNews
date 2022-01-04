using Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.BLL.DataTransferObjects.SectionsDto;
using News.BLL.Interfaces;
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
        private readonly ISectionServices _sectionsService;
        public SectionController(ILoggerManager logger, ISectionServices section)
        {
            _logger = logger;
            _sectionsService = section;
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
            var sectionDto = await _sectionsService.GetSections();
            return Ok(sectionDto);
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
            var sectionDto = await _sectionsService.GetSection(id);
            if (sectionDto == null)
            {
                _logger.LogError($"Section with id: {id} doesn't exist");
                return NotFound($"Section with id: {id} doesn't exist");
            }
            return Ok(sectionDto);
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
            if (section == null)
            {
                _logger.LogError("SectionForCreationDto object is null");
                return BadRequest("SectionForCreationDto object is null");
            }
            var sectionDto = await _sectionsService.CreateSection(section);
            return NoContent();
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
            var sectionDto = await _sectionsService.DeleteSection(id);
            if (sectionDto == false)
            {
                _logger.LogError("sectionDto object is null");
                return BadRequest("sectionDto object is null");
            }
            return NoContent();
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
            if (section == null)
            {
                _logger.LogError("SectionForCreationDto object sent from client is null.");
                return BadRequest("SectionForCreationDto object is null");
            }
            var sectionDto = await _sectionsService.UpdateSection(id, section);
            if (sectionDto == false)
            {
                _logger.LogInfo($"Section with id: {id} doesn't exist.");
                return NotFound($"Section with id: {id} doesn't exist");
            }
            return NoContent();
        }
    }
}
