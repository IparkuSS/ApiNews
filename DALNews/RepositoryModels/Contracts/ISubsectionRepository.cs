using News.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace News.DAL.RepositoryModels.Contracts
{
    public interface ISubsectionRepository
    {
        Task<IEnumerable<Subsection>> GetSubsectionsAsync(Guid sectionId);

        Task<Subsection> GetSubsectionAsync(Guid sectionId, Guid id);

        void CreateSubsectionForSection(Guid sectionId, Subsection subsection);

        void DeleteSubsection(Subsection subsection);
    }
}
