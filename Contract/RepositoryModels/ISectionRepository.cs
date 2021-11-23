using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface ISectionRepository
    {
       
        Task<IEnumerable<Section>> GetAllSectionAsync(bool trackChanges);
        Task<Section> GetSectionAsync(Guid companyId, bool trackChanges);
        void CreateSection(Section section);
        Task<IEnumerable<Section>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteSection(Section section);

    }
}
