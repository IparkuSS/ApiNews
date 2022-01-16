using AutoMapper;
using News.BLL.DataTransferObjects.SectionsDto;
using News.BLL.Interfaces;
using News.DAL.Models;
using News.DAL.RepositoryModels.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace News.BLL.Services
{
    /// <summary>
    /// the service class that serves the sections controller
    /// </summary>
    public class SectionServices : ISectionServices
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper _mapper;
        public SectionServices(IMapper mapper, ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SectionDto>> GetSections()
        {
            var sections = await _sectionRepository.GetAllSectionAsync();
            var sectionDto = _mapper.Map<IEnumerable<SectionDto>>(sections);
            return sectionDto;
        }
        public async Task<SectionDto> GetSection(Guid id)
        {
            var section = await _sectionRepository.GetSectionAsync(id);
            if (section == null)
            {
                return null;
            }
            else
            {
                var companyDto = _mapper.Map<SectionDto>(section);
                return companyDto;
            }
        }
        public async Task<bool> CreateSection(SectionForCreationDto sectionForCreationDto)
        {
            if (sectionForCreationDto == null)
            {
                return false;
            }
            var sectionEntity = _mapper.Map<Section>(sectionForCreationDto);
            _sectionRepository.CreateSection(sectionEntity);
            return true;
        }
        public async Task<bool> DeleteSection(Guid id)
        {
            var section = await _sectionRepository.GetSectionAsync(id);
            if (section == null)
            {
                return false;
            }
            _sectionRepository.DeleteSection(section);
            return true;
        }
        public async Task<bool> UpdateSection(Guid id, SectionForUpdateDto sectionForUpdateDto)
        {
            var sectionEntity = await _sectionRepository.GetSectionAsync(id);
            if (sectionEntity == null)
            {
                return false;
            }
            _mapper.Map(sectionForUpdateDto, sectionEntity);
            return true;
        }
    }
}
