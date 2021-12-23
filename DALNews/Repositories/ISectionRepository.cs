using DALNews.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DALNews.Repositories
{
    public interface ISectionRepository
    {
        Task<IEnumerable<Section>> GetAllSectionAsync(bool trackChanges);
        Task<Section> GetSectionAsync(Guid sectionId, bool trackChanges);
        void CreateSection(Section section);
        Task<List<Section>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteSection(Section section);
        void SaveSection();
    }
}
