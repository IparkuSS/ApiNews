using System.Threading.Tasks;
namespace Contract.Repositories
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
