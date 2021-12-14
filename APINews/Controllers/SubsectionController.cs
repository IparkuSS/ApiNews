using AutoMapper;
using Contract;
using Contract.Repositories;
using Entities.DataTransferObjects.SubsectionsDto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace APINews.Controllers
{
    [Route("api/section/{sectionId}/subsection")]
    [ApiController]
    public class SubsectionController : Controller
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public SubsectionController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
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
                var section = await _repository.Section.GetSectionAsync(sectionId, trackChanges: false);
                if (section == null)
                {
                    _logger.LogInfo($"section with id: {sectionId} doesn't exist.");
                    return StatusCode(404, $"Section with id: {sectionId} doesn't exist");
                }
                var subsectionFromDb = await _repository.Subsection.GetSubsectionsAsync(sectionId, trackChanges: false);
                var subsectionDto = _mapper.Map<IEnumerable<SubsectionDto>>(subsectionFromDb);
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
                var section = await _repository.Section.GetSectionAsync(sectionId, trackChanges: false);
                if (section == null)
                {
                    _logger.LogInfo($"Section with id: {sectionId} doesn't exist.");
                    return StatusCode(404, $"Section with id: {sectionId} doesn't exist");
                }
                var subsectionDb = await _repository.Subsection.GetSubsectionAsync(sectionId, id, trackChanges: false);
                if (subsectionDb == null)
                {
                    _logger.LogInfo($"Subsection with id: {id} doesn't exist.");
                    return StatusCode(404, $"Subsection with id: {id} doesn't exist");
                }
                var subsection = _mapper.Map<SubsectionDto>(subsectionDb);
                return Ok(subsection);
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
                var section = await _repository.Section.GetSectionAsync(sectionId, trackChanges: false);
                if (section == null)
                {
                    _logger.LogInfo($"Section with id: {sectionId} doesn't exist.");
                    return StatusCode(404, $"Section with id: {sectionId} doesn't exist");
                }
                var subsectionEntity = _mapper.Map<Subsection>(subsection);
                _repository.Subsection.CreateSubsectionForSection(sectionId, subsectionEntity);
                await _repository.SaveAsync();
                return Ok();
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
                var section = await _repository.Section.GetSectionAsync(sectionId, trackChanges: false);
                if (section == null)
                {
                    _logger.LogInfo($"Section with id: {sectionId} doesn't exist.");
                    return StatusCode(404, $"Section with id: {sectionId} doesn't exist ");
                }
                var SubsectionForsection = await _repository.Subsection.GetSubsectionAsync(sectionId, id, trackChanges: false);
                if (SubsectionForsection == null)
                {
                    _logger.LogInfo($"Subsection with id: {id} doesn't exist");
                    return StatusCode(404, $"Subsection with id: {id} doesn't exist");
                }
                _repository.Subsection.DeleteSubsection(SubsectionForsection);
                await _repository.SaveAsync();
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
                var section = await _repository.Section.GetSectionAsync(sectionId, trackChanges: false);
                if (section == null)
                {
                    _logger.LogInfo($"Section with id: {sectionId} doesn't exist.");
                    return StatusCode(404, $"Section with id: {sectionId} doesn't exist");
                }
                var subsectionEntity = await _repository.Subsection.GetSubsectionAsync(sectionId, id, trackChanges: true);
                if (subsectionEntity == null)
                {
                    _logger.LogInfo($"Subsection with id: {id} doesn't exist.");
                    return StatusCode(404, $"Subsection with id: {id} doesn't exist");
                }
                _mapper.Map(subsection, subsectionEntity);
                await _repository.SaveAsync();
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
