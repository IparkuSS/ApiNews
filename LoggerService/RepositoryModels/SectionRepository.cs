using Contract;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerService.RepositoryModels
{
    public class SectionRepository : RepositoryBase<Section>, ISectionRepository
    {
        public SectionRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Section>> GetAllSectionAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.NameSection).ToListAsync();





        public async Task<Section> GetSectionAsync(Guid idsection, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(idsection), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateSection(Section section) => Create(section);

        public async Task<IEnumerable<Section>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) => 
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteSection(Section section) { Delete(section); }

    }
}
