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
    public class SubsectionRepository : RepositoryBase<Subsection>, ISubsectionRepository
    {
        public SubsectionRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Subsection>> GetSubsectionsAsync(Guid Idsection, bool trackChanges) =>
            await FindByCondition(e => e.IdSection.Equals(Idsection), trackChanges).ToListAsync();

        public async Task<Subsection> GetSubsectionAsync(Guid idsection, Guid id, bool trackChanges) => 
            await FindByCondition(e => e.IdSection.Equals(idsection) && e.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();


        public void CreateSubsectionForSection(Guid sectionId, Subsection subsection) 
        { 
            subsection.IdSection = sectionId; Create(subsection);
        }

        public void DeleteSubsection(Subsection subsection) { Delete(subsection); }
    }
}
