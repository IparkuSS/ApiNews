using APINews.ModelBinders;
using AutoMapper;
using Contract;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APINews.Controllers
{
    [Route("api/section")]
    [ApiController]
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var sections = await _repository.Section.GetAllSectionAsync(trackChanges: false);

                var SectionDto = _mapper.Map<IEnumerable<SectionDto>>(sections);
                return Ok(SectionDto);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(Get)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}", Name = "SectionById")]
        public async Task<IActionResult> GetSection(Guid id)
        {
            try
            {
                var company = await _repository.Section.GetSectionAsync(id, trackChanges: false);
                if (company == null)
                {
                    _logger.LogInfo($"Section with id: {id} doesn't exist in the database..");
                    return NotFound();
                }
                else
                {
                    var companyDto = _mapper.Map<SectionDto>(company);
                    return Ok(companyDto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetSection)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("collection/({ids})", Name = "SectionCollection")]
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
                    return NotFound();
                }
                var companiesToReturn = _mapper.Map<IEnumerable<SectionDto>>(sectionEntities);
                return Ok(companiesToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetSectionCollection)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("collection")]
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
                _logger.LogError($"Something went wrong in the {nameof(CreateCompanyCollection)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] SectionForCreationDto section)
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

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateCompany)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(Guid id)
        {
            try
            {
                var section = await _repository.Section.GetSectionAsync(id, trackChanges: false);
                if (section == null)
                {
                    _logger.LogInfo($"Section with id: {id} doesn't exist in the database.");
                    return NotFound();
                }

                _repository.Section.DeleteSection(section);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(DeleteSection)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] SectionForUpdateDto section)
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
                    _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                    return NotFound();
                }

                _mapper.Map(section, companyEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateCompany)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }
    }
}
