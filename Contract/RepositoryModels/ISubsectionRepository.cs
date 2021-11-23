using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface ISubsectionRepository
    {
        Task<IEnumerable<Subsection>> GetSubsectionsAsync(Guid Idsection, bool trackChanges);
        Task<Subsection> GetSubsectionAsync(Guid idsection, Guid id, bool trackChanges);
        void CreateSubsectionForSection(Guid idsection, Subsection employee);

        void DeleteSubsection(Subsection employee);
    }
}
