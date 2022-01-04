using System.Threading.Tasks;
namespace News.DAL.Repositories
{
    public interface IRepositoryManager
    {
        IArticleRepository Article { get; }
        IAuthorRepository Author { get; }
        ISectionRepository Section { get; }
        ISubsectionRepository Subsection { get; }
    }
}
