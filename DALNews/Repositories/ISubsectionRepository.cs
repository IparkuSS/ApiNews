using News.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace News.DAL.Repositories
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
