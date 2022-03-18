using News.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using News.DAL.Parameters;

namespace News.DAL.RepositoryModels.Contracts
{
    public interface ISubsectionRepository
    {
        Task<IEnumerable<Subsection>> GetSubsectionsAsync(Guid sectionId, SubsectionParameters subsectionParameters);

        Task<Subsection> GetSubsectionAsync(Guid sectionId, Guid id);

        void CreateSubsectionForSection(Guid sectionId, Subsection subsection);

        void DeleteSubsection(Subsection subsection);
        void SaveSubsection();
    }
}
