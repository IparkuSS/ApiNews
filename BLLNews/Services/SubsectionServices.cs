using AutoMapper;
using News.BLL.DataTransferObjects.SubsectionsDto;
using News.BLL.Interfaces;
using News.DAL.Models;
using News.DAL.RepositoryModels.Contracts;
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
        private readonly ISubsectionRepository _subsectionRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper _mapper;
        public SubsectionServices(IMapper mapper, ISubsectionRepository subsectionRepository, ISectionRepository sectionRepository)
        {
            _subsectionRepository = subsectionRepository;
            _sectionRepository = sectionRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SubsectionDto>> GetSubsectionsForSection(Guid sectionId)
        {
            var section = await _sectionRepository.GetSectionAsync(sectionId);
            if (section == null)
            {
                return null;
            }
            var subsectionFromDb = await _subsectionRepository.GetSubsectionsAsync(sectionId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Subsection, SubsectionDto>()).CreateMapper();
            var subsectionDto = mapper.Map<IEnumerable<SubsectionDto>>(subsectionFromDb);
            return subsectionDto;
        }
        public async Task<SubsectionDto> GetSubsectionForSection(Guid id, Guid sectionId)
        {
            var section = await _sectionRepository.GetSectionAsync(sectionId);
            var subsectionDb = await _subsectionRepository.GetSubsectionAsync(sectionId, id);
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
            var section = await _sectionRepository.GetSectionAsync(sectionId);
            if (section == null)
            {
                return false;
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubsectionForCreationDto, Subsection>()).CreateMapper();
            var subsectionEntity = mapper.Map<Subsection>(subsectionForCreationDto);
            _subsectionRepository.CreateSubsectionForSection(sectionId, subsectionEntity);
            return true;
        }
        public async Task<bool> DeleteSubsectionForSection(Guid id, Guid sectionId)
        {
            var section = await _sectionRepository.GetSectionAsync(sectionId);
            var SubsectionForsection = await _subsectionRepository.GetSubsectionAsync(sectionId, id);
            if (SubsectionForsection == null)
            {
                return true;
            }
            _subsectionRepository.DeleteSubsection(SubsectionForsection);
            return false;
        }
        public async Task<bool> UpdateSubsectionForSection(Guid id, Guid sectionId, SubsectionForUpdateDto subsectionForUpdateDto)
        {
            var section = await _sectionRepository.GetSectionAsync(sectionId);
            var subsectionEntity = await _subsectionRepository.GetSubsectionAsync(sectionId, id);
            if (subsectionEntity == null)
            {
                return false;
            }
            _mapper.Map(subsectionForUpdateDto, subsectionEntity);
            return true;
        }
    }
}
