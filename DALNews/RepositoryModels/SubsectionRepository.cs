using Microsoft.EntityFrameworkCore;
using News.DAL.Models;
using News.DAL.RepositoryModels.Contracts;
using News.DAL.Setting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.DAL.RepositoryModels
{
    public class SubsectionRepository : RepositoryBase<Subsection>, ISubsectionRepository
    {
        private readonly TrackSettings _trackSettings;
        public SubsectionRepository(RepositoryContext repositoryContext, TrackSettings trackSettings) : base(repositoryContext) { _trackSettings = trackSettings; }
        public async Task<IEnumerable<Subsection>> GetSubsectionsAsync(Guid sectionId) =>
            await FindByCondition(e => e.IdSection.Equals(sectionId), _trackSettings.TrackChanges).ToListAsync();

        public async Task<Subsection> GetSubsectionAsync(Guid sectionId, Guid id) =>
            await FindByCondition(e => e.IdSection.Equals(sectionId) && e.Id.Equals(id), _trackSettings.TrackChanges)
            .SingleOrDefaultAsync();

        public void CreateSubsectionForSection(Guid sectionId, Subsection subsection)
        {
            subsection.IdSection = sectionId;
            Create(subsection);
        }

        public void DeleteSubsection(Subsection subsection) => Delete(subsection);


    }
}
