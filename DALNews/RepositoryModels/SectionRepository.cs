using Microsoft.EntityFrameworkCore;
using News.DAL.Models;
using News.DAL.RepositoryModels.Contracts;
using News.DAL.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace News.DAL.RepositoryModels
{
    public class SectionRepository : RepositoryBase<Section>, ISectionRepository
    {
        private readonly TrackSettings _trackSettings;
        public SectionRepository(RepositoryContext repositoryContext, TrackSettings trackSettings) : base(repositoryContext) { _trackSettings = trackSettings; }
        public async Task<IEnumerable<Section>> GetAllSectionAsync() =>
            await FindAll(_trackSettings.TrackChanges)
            .OrderBy(c => c.NameSection).ToListAsync();

        public async Task<Section> GetSectionAsync(Guid sectionId) =>
            await FindByCondition(c => c.Id.Equals(sectionId), _trackSettings.TrackChanges)
            .SingleOrDefaultAsync();

        public void CreateSection(Section section) => Create(section);


        public void SaveSectiom() => Save();

        public async Task<List<Section>> GetByIdsAsync(IEnumerable<Guid> ids) =>
            await FindByCondition(x => ids.Contains(x.Id), _trackSettings.TrackChanges).ToListAsync();

        public void DeleteSection(Section section) => Delete(section);


    }
}
