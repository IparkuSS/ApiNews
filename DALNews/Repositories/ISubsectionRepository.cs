using DALNews.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DALNews.Repositories
{
    public interface ISubsectionRepository
    {
        Task<IEnumerable<Subsection>> GetSubsectionsAsync(Guid sectionId, bool trackChanges);
        Task<Subsection> GetSubsectionAsync(Guid sectionId, Guid id, bool trackChanges);
        void CreateSubsectionForSection(Guid sectionId, Subsection subsection);
        void DeleteSubsection(Subsection subsection);
        void SaveSubsection();
    }
}
