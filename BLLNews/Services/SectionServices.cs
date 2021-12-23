﻿using AutoMapper;
using BLLNews.DataTransferObjects.SectionsDto;
using BLLNews.Interfaces;
using DALNews.Models;
using DALNews.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BLLNews.Services
{
    /// <summary>
    /// the service class that serves the sections controller
    /// </summary>
    public class SectionServices : ISectionServices
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public SectionServices(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SectionDto>> GetSections()
        {
            var sections = await _repository.Section.GetAllSectionAsync(trackChanges: false);
            var sectionDto = _mapper.Map<IEnumerable<SectionDto>>(sections);
            return sectionDto;
        }
        public async Task<SectionDto> GetSection(Guid id)
        {
            var section = await _repository.Section.GetSectionAsync(id, trackChanges: false);
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
            _repository.Section.CreateSection(sectionEntity);
            _repository.Section.SaveSection();
            return true;
        }
        public async Task<string> DeleteSection(Guid id)
        {
            var section = await _repository.Section.GetSectionAsync(id, trackChanges: false);
            if (section == null)
            {
                return null;
            }
            _repository.Section.DeleteSection(section);
            _repository.Section.SaveSection();
            return "Ok";
        }
        public async Task<string> UpdateSection(Guid id, SectionForUpdateDto sectionForUpdateDto)
        {
            var sectionEntity = await _repository.Section.GetSectionAsync(id, trackChanges: true);
            if (sectionEntity == null)
            {
                return null;
            }
            _mapper.Map(sectionForUpdateDto, sectionEntity);
            _repository.Section.SaveSection();
            return "Ok";
        }
    }
}