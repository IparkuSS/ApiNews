using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IRepositoryManager
    {
        IArticleRepository Article { get; }
        IAuthorRepository Author { get; }
        ISectionRepository Section { get; }
        ISubsectionRepository Subsection { get; }
        Task SaveAsync();
    }
}
