using DALNews.Repositories;
using DALNews;
using DALNews.RepositoryModels;
using System.Threading.Tasks;
namespace DALNews
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IArticleRepository _articleRepository;
        private IAuthorRepository _authorRepository;
        private ISectionRepository _sectionRepository;
        private ISubsectionRepository _subsectionRepository;
        public RepositoryManager(RepositoryContext repositoryContext) { _repositoryContext = repositoryContext; }
        public IArticleRepository Article
        {
            get
            {
                if (_articleRepository == null) _articleRepository = new ArticleRepository(_repositoryContext);
                return _articleRepository;
            }
        }
        public IAuthorRepository Author
        {
            get
            {
                if (_authorRepository == null) _authorRepository = new AuthorRepository(_repositoryContext);
                return _authorRepository;
            }
        }
        public ISectionRepository Section
        {
            get
            {
                if (_sectionRepository == null) _sectionRepository = new SectionRepository(_repositoryContext);
                return _sectionRepository;
            }
        }
        public ISubsectionRepository Subsection
        {
            get
            {
                if (_subsectionRepository == null) _subsectionRepository = new SubsectionRepository(_repositoryContext);
                return _subsectionRepository;
            }
        }
    }
}
