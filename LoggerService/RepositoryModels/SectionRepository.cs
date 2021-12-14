﻿using Contract.Repositories;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace LoggerService.RepositoryModels
{
    public class SectionRepository : RepositoryBase<Section>, ISectionRepository
    {
        public SectionRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
        public async Task<IEnumerable<Section>> GetAllSectionAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.NameSection).ToListAsync();
        public async Task<Section> GetSectionAsync(Guid sectionId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(sectionId), trackChanges)
            .SingleOrDefaultAsync();
        public void CreateSection(Section section) => Create(section);
        public async Task<List<Section>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        public void DeleteSection(Section section) => Delete(section);
    }
}