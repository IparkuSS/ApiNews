using Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.BLL.DataTransferObjects.SubsectionsDto;
using News.BLL.Interfaces;
using System;
using System.Threading.Tasks;
namespace APINews.Controllers
{
    [Route("api/section/{sectionId}/subsection")]
    [ApiController]
    public class SubsectionController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly ISubsectionServices _subsectionService;
        public SubsectionController(ILoggerManager logger, ISubsectionServices subsection)
        {
            _logger = logger;
            _subsectionService = subsection;
        }
        /// <summary>
        /// Returns all subsection for section
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSubsectionsForSection(Guid sectionId)
        {
            var subsectionDto = await _subsectionService.GetSubsectionsForSection(sectionId);
            if (subsectionDto == null)
            {
                _logger.LogInfo($"section with id: {sectionId} doesn't exist.");
                return NotFound($"section with id: {sectionId} doesn't exist");
            }
            return Ok(subsectionDto);
        }
        /// <summary>
        /// Returns specified subsection by id for section
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetSubsectionForSection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSubsectionForSection(Guid sectionId, Guid id)
        {
            var sectionDto = await _subsectionService.GetSubsectionForSection(id, sectionId);
            if (sectionDto == null)
            {
                _logger.LogInfo($"Subsection with id: {id} doesn't exist.");
                return NotFound($"Subsection with id: {id} doesn't exist");
            }
            return Ok(sectionDto);
        }
        /// <summary>
        /// Creates a new subsection for section
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="subsection"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSubsectionForSection(Guid sectionId, [FromBody] SubsectionForCreationDto subsection)
        {
            if (subsection == null)
            {
                _logger.LogError("SubsectionForCreationDto object sent from client is null.");
                return BadRequest("SubsectionForCreationDto object is null");
            }
            var subsectionDto = await _subsectionService.CreateSubsectionForSection(sectionId, subsection);
            if (subsectionDto == false)
            {
                _logger.LogInfo($"Section with id: {sectionId} doesn't exist.");
                return NotFound($"Section with id: {sectionId} doesn't exist");
            }
            return NoContent();
        }
        /// <summary>
        /// Deletes a subsection from a section
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSubsectionForSection(Guid sectionId, Guid id)
        {
            var subsectionDto = await _subsectionService.DeleteSubsectionForSection(id, sectionId);
            if (subsectionDto == false)
            {
                _logger.LogInfo($"Subsection with id: {id} doesn't exist");
                return NotFound($"Subsection with id: {id} doesn't exist");
            }
            return NoContent();
        }
        /// <summary>
        /// Updates a subsection for section
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="id"></param>
        /// <param name="subsection"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSubsectionForSection(Guid sectionId, Guid id, [FromBody] SubsectionForUpdateDto subsection)
        {
            if (subsection == null)
            {
                _logger.LogError("SubsectionForUpdateDto object sent from client is null.");
                return BadRequest("SubsectionForUpdateDto object is null");
            }
            var subsectionDto = await _subsectionService.UpdateSubsectionForSection(id, sectionId, subsection);
            if (subsectionDto == false)
            {
                _logger.LogInfo($"Subsection with id: {id} doesn't exist");
                return NotFound($"Subsection with id: {id} doesn't exist");
            }
            return NoContent();
        }
    }
}
