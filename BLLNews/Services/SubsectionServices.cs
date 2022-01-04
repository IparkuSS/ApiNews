using AutoMapper;
using News.BLL.DataTransferObjects.SubsectionsDto;
using News.BLL.Interfaces;
using News.DAL.Models;
using News.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace News.BLL.Services
{
    /// <summary>
    /// the service class that serves the subsections controller
    /// </summary>
    public class SubsectionServices : ISubsectionServices
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public SubsectionServices(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SubsectionDto>> GetSubsectionsForSection(Guid sectionId)
        {
            var section = await _repository.Section.GetSectionAsync(sectionId, trackChanges: false);
            if (section == null)
            {
                return null;
            }
            var subsectionFromDb = await _repository.Subsection.GetSubsectionsAsync(sectionId, trackChanges: false);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Subsection, SubsectionDto>()).CreateMapper();
            var subsectionDto = mapper.Map<IEnumerable<SubsectionDto>>(subsectionFromDb);
            return subsectionDto;
        }
        public async Task<SubsectionDto> GetSubsectionForSection(Guid id, Guid sectionId)
        {
            var section = await _repository.Section.GetSectionAsync(sectionId, trackChanges: false);
            var subsectionDb = await _repository.Subsection.GetSubsectionAsync(sectionId, id, trackChanges: false);
            if (subsectionDb == null)
            {
                return null;
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Subsection, SubsectionDto>()).CreateMapper();
            var subsection = mapper.Map<SubsectionDto>(subsectionDb);
            return subsection;
        }
        public async Task<bool> CreateSubsectionForSection(Guid sectionId, SubsectionForCreationDto subsectionForCreationDto)
        {
            var section = await _repository.Section.GetSectionAsync(sectionId, trackChanges: false);
            if (section == null)
            {
                return false;
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubsectionForCreationDto, Subsection>()).CreateMapper();
            var subsectionEntity = mapper.Map<Subsection>(subsectionForCreationDto);
            _repository.Subsection.CreateSubsectionForSection(sectionId, subsectionEntity);
            _repository.Subsection.SaveSubsection();
            return true;
        }
        public async Task<bool> DeleteSubsectionForSection(Guid id, Guid sectionId)
        {
            var section = await _repository.Section.GetSectionAsync(sectionId, trackChanges: false);
            var SubsectionForsection = await _repository.Subsection.GetSubsectionAsync(sectionId, id, trackChanges: false);
            if (SubsectionForsection == null)
            {
                return true;
            }
            _repository.Subsection.DeleteSubsection(SubsectionForsection);
            _repository.Subsection.SaveSubsection();
            return false;
        }
        public async Task<bool> UpdateSubsectionForSection(Guid id, Guid sectionId, SubsectionForUpdateDto subsectionForUpdateDto)
        {
            var section = await _repository.Section.GetSectionAsync(sectionId, trackChanges: false);
            var subsectionEntity = await _repository.Subsection.GetSubsectionAsync(sectionId, id, trackChanges: true);
            if (subsectionEntity == null)
            {
                return false;
            }
            _mapper.Map(subsectionForUpdateDto, subsectionEntity);
            _repository.Subsection.SaveSubsection();
            return true;
        }
    }
}
