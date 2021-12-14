using APINews.ModelBinders;
using AutoMapper;
using Contract;
using Contract.Repositories;
using Entities.DataTransferObjects.SectionsDto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace APINews.Controllers
{
    [Route("api/section")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class SectionController : Controller
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public SectionController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
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
                var sections = await _repository.Section.GetAllSectionAsync(trackChanges: false);
                var sectionDto = _mapper.Map<IEnumerable<SectionDto>>(sections);
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
                var section = await _repository.Section.GetSectionAsync(id, trackChanges: false);
                if (section == null)
                {
                    _logger.LogInfo($"Section with id: {id} doesn't exist");
                    return StatusCode(404, $"Section with id: {id} doesn't exist");
                }
                else
                {
                    var companyDto = _mapper.Map<SectionDto>(section);
                    return Ok(companyDto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetSection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Returns the specified section with its subsection by id 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet("collection/({ids})", Name = "SectionCollection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSectionCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            try
            {
                if (ids == null)
                {
                    _logger.LogError("Parameter ids is null");
                    return BadRequest("Parameter ids is null");
                }
                var sectionEntities = await _repository.Section.GetByIdsAsync(ids, trackChanges: false);
                if (ids.Count() != sectionEntities.Count())
                {
                    _logger.LogError("Some ids are not valid in a collection");
                    return StatusCode(404, "Some ids are not valid in a collection");
                }
                var companiesToReturn = _mapper.Map<IEnumerable<SectionDto>>(sectionEntities);
                return Ok(companiesToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetSectionCollection)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Creates a new section with a subsection
        /// </summary>
        /// <param name="sectionCollection"></param>
        /// <returns></returns>
        [HttpPost("collection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<SectionForCreationDto> sectionCollection)
        {
            try
            {
                if (sectionCollection == null)
                {
                    _logger.LogError("Section collection sent from client is null.");
                    return BadRequest("Section collection is null");
                }
                var sectionEntities = _mapper.Map<IEnumerable<Section>>(sectionCollection);
                foreach (var section in sectionEntities)
                {
                    _repository.Section.CreateSection(section);
                }
                await _repository.SaveAsync();
                var companyCollectionToReturn = _mapper.Map<IEnumerable<SectionDto>>(sectionEntities);
                var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));
                return CreatedAtRoute("SectionCollection", new { ids }, companyCollectionToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateCompanyCollection)} action {ex}, {ex.Message}");
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
                    _logger.LogError("SectionForCreationDto object sent from client is null.");
                    return BadRequest("SectionForCreationDto object is null");
                }
                var sectionEntity = _mapper.Map<Section>(section);
                _repository.Section.CreateSection(sectionEntity);
                await _repository.SaveAsync();
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSection(Guid id)
        {
            try
            {
                var section = await _repository.Section.GetSectionAsync(id, trackChanges: false);
                if (section == null)
                {
                    _logger.LogInfo($"Section with id: {id} doesn't exist.");
                    return StatusCode(404, $"Section with id: {id} doesn't exist");
                }
                _repository.Section.DeleteSection(section);
                await _repository.SaveAsync();
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
                    _logger.LogError("CompanyForUpdateDto object sent from client is null.");
                    return BadRequest("CompanyForUpdateDto object is null");
                }
                var companyEntity = await _repository.Section.GetSectionAsync(id, trackChanges: true);
                if (companyEntity == null)
                {
                    _logger.LogInfo($"Section with id: {id} doesn't exist.");
                    return StatusCode(404, $"Section with id: {id} doesn't exist");
                }
                _mapper.Map(section, companyEntity);
                await _repository.SaveAsync();
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
