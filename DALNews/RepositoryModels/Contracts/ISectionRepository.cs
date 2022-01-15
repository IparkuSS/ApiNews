using News.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace News.DAL.RepositoryModels.Contracts
{
    public interface ISectionRepository
    {
        Task<IEnumerable<Section>> GetAllSectionAsync();

        Task<Section> GetSectionAsync(Guid sectionId);

        void CreateSection(Section section);

        Task<List<Section>> GetByIdsAsync(IEnumerable<Guid> ids);

        void DeleteSection(Section section);

    }
}
