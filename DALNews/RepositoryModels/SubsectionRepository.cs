using Microsoft.EntityFrameworkCore;
using News.DAL.Models;
using News.DAL.RepositoryModels.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.DAL.RepositoryModels
{
    public class SubsectionRepository : RepositoryBase<Subsection>, ISubsectionRepository
    {
        public SubsectionRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
        public async Task<IEnumerable<Subsection>> GetSubsectionsAsync(Guid sectionId, bool trackChanges) =>
            await FindByCondition(e => e.IdSection.Equals(sectionId), trackChanges).ToListAsync();

        public async Task<Subsection> GetSubsectionAsync(Guid sectionId, Guid id, bool trackChanges) =>
            await FindByCondition(e => e.IdSection.Equals(sectionId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateSubsectionForSection(Guid sectionId, Subsection subsection)
        {
            subsection.IdSection = sectionId;
            Create(subsection);
        }

        public void DeleteSubsection(Subsection subsection) => Delete(subsection);


    }
}
