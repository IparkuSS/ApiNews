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
    [Route("api/section/{idsection}/subsection")]
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

        [HttpGet]
        public async Task<IActionResult> GetSubsectionForSection(Guid idsection)
        {
            try
            {
                var section = await _repository.Section.GetSectionAsync(idsection, trackChanges: false);
                if (section == null)
                {
                    _logger.LogInfo($"section with id: {idsection} doesn't exist in the database.");
                    return NotFound();
                }

                var subsectionFromDb = await _repository.Subsection.GetSubsectionsAsync(idsection, trackChanges: false);

                var subsectionDto = _mapper.Map<IEnumerable<SubsectionDto>>(subsectionFromDb);

                return Ok(subsectionDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetSubsectionForSection)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetSubsectionForSection")]
        public async Task<IActionResult> GetSubsectionForSection(Guid idsection, Guid id)
        {
            try
            {
                var section = await _repository.Section.GetSectionAsync(idsection, trackChanges: false);
                if (section == null)
                {
                    _logger.LogInfo($"Section with id: {idsection} doesn't exist in the database.");
                    return NotFound();
                }

                var subsectionDb = await _repository.Subsection.GetSubsectionAsync(idsection, id, trackChanges: false);
                if (subsectionDb == null)
                {
                    _logger.LogInfo($"Subsection with id: {id} doesn't exist in the database.");
                    return NotFound();
                }

                var subsection = _mapper.Map<SubsectionDto>(subsectionDb);

                return Ok(subsection);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetSubsectionForSection)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateSubsectionForSection(Guid idsection, [FromBody] SubsectionForCreationDto subsection)
        {
            try
            {
                if (subsection == null)
                {
                    _logger.LogError("SubsectionForCreationDto object sent from client is null.");
                    return BadRequest("SubsectionForCreationDto object is null");
                }

                var section = await _repository.Section.GetSectionAsync(idsection, trackChanges: false);
                if (section == null)
                {
                    _logger.LogInfo($"Section with id: {idsection} doesn't exist in the database.");
                    return NotFound();
                }

                var subsectionEntity = _mapper.Map<Subsection>(subsection);
                _repository.Subsection.CreateSubsectionForSection(idsection, subsectionEntity);
                await _repository.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateSubsectionForSection)} action {ex}");

                return StatusCode(500, "Internal server error");
            }

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubsectionForSection(Guid idsection, Guid id)
        {
            try
            {
                var company = await _repository.Section.GetSectionAsync(idsection, trackChanges: false);
                if (company == null)
                {
                    _logger.LogInfo($"Section with id: {idsection} doesn't exist in the database.");
                    return NotFound();
                }

                var SubsectionForsection = await _repository.Subsection.GetSubsectionAsync(idsection, id, trackChanges: false);
                if (SubsectionForsection == null)
                {
                    _logger.LogInfo($"Subsection with id: {id} doesn't exist in the database.");
                    return NotFound();
                }

                _repository.Subsection.DeleteSubsection(SubsectionForsection);
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
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid idsection, Guid id, [FromBody] SubsectionForUpdateDto employee)
        {
            try
            {
                if (employee == null)
                {
                    _logger.LogError("SubsectionForUpdateDto object sent from client is null.");
                    return BadRequest("SubsectionForUpdateDto object is null");
                }

                var section = await _repository.Section.GetSectionAsync(idsection, trackChanges: false);
                if (section == null)
                {
                    _logger.LogInfo($"Section with id: {idsection} doesn't exist in the database.");
                    return NotFound();
                }

                var sectionEntity = await _repository.Subsection.GetSubsectionAsync(idsection, id, trackChanges: true);
                if (sectionEntity == null)
                {
                    _logger.LogInfo($"Subsection with id: {id} doesn't exist in the database.");
                    return NotFound();
                }

                _mapper.Map(employee, sectionEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateEmployeeForCompany)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }
    }
}
