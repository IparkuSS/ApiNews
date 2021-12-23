using BLLNews.DataTransferObjects.SubsectionsDto;
using BLLNews.Interfaces;
using Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace APINews.Controllers
{
    [Route("api/section/{sectionId}/subsection")]
    [ApiController]
    public class SubsectionController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly ISubsectionServices _subsection;
        public SubsectionController(ILoggerManager logger, ISubsectionServices subsection)
        {
            _logger = logger;
            _subsection = subsection;
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
            try
            {
                var subsectionDto = await _subsection.GetSubsectionsForSection(sectionId);
                if (subsectionDto == null)
                {
                    _logger.LogInfo($"section with id: {sectionId} doesn't exist.");
                    return StatusCode(404, $"section with id: {sectionId} doesn't exist");
                }
                return Ok(subsectionDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetSubsectionsForSection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
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
            try
            {
                var sectionDto = await _subsection.GetSubsectionForSection(id, sectionId);
                if (sectionDto == null)
                {
                    _logger.LogInfo($"Subsection with id: {id} doesn't exist.");
                    return StatusCode(404, $"Subsection with id: {id} doesn't exist");
                }
                return Ok(sectionDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetSubsectionForSection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
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
            try
            {
                if (subsection == null)
                {
                    _logger.LogError("SubsectionForCreationDto object sent from client is null.");
                    return BadRequest("SubsectionForCreationDto object is null");
                }
                var subsectionDto = await _subsection.CreateSubsectionForSection(sectionId, subsection);
                if (subsectionDto == null)
                {
                    _logger.LogInfo($"Section with id: {sectionId} doesn't exist.");
                    return StatusCode(404, $"Section with id: {sectionId} doesn't exist");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateSubsectionForSection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
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
            try
            {
                var subsectionDto = await _subsection.DeleteSubsectionForSection(id, sectionId);
                if (subsectionDto == null)
                {
                    _logger.LogInfo($"Subsection with id: {id} doesn't exist");
                    return StatusCode(404, $"Subsection with id: {id} doesn't exist");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(DeleteSubsectionForSection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
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
            try
            {
                if (subsection == null)
                {
                    _logger.LogError("SubsectionForUpdateDto object sent from client is null.");
                    return BadRequest("SubsectionForUpdateDto object is null");
                }
                var subsectionDto = await _subsection.UpdateSubsectionForSection(id, sectionId, subsection);
                if (subsectionDto == null)
                {
                    _logger.LogInfo($"Subsection with id: {id} doesn't exist");
                    return StatusCode(404, $"Subsection with id: {id} doesn't exist");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateSubsectionForSection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
