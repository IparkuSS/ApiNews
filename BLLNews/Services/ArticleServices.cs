using AutoMapper;
using News.BLL.DataTransferObjects.ArticlesDto;
using News.BLL.Interfaces;
using News.DAL.Models;
using News.DAL.RepositoryModels.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace News.BLL.Services
{
    /// <summary>
    /// the service class that serves the articles controller
    /// </summary>
    public class ArticleServices : IArticleServices
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ISubsectionRepository _subsectionRepository;
        private readonly IMapper _mapper;
        public ArticleServices(IArticleRepository articleRepository, IMapper mapper, ISubsectionRepository subsectionRepository)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _subsectionRepository = subsectionRepository;
        }
        public async Task<IEnumerable<ArticleDto>> GetAriclesForSubsection(Guid sectionId, Guid subsectionId)
        {
            var article = await _subsectionRepository.GetSubsectionAsync(sectionId, subsectionId, trackChanges: false);
            if (article == null)
            {
                return null;
            }
            var articleFromDb = await _articleRepository.GetArticlesAsync(sectionId, subsectionId, trackChanges: false);
            var articleDto = _mapper.Map<IEnumerable<ArticleDto>>(articleFromDb);
            return articleDto;
        }
        public async Task<ArticleDto> GetAricleForSubsection(Guid sectionId, Guid subsectionId, Guid id)
        {
            var article = await _subsectionRepository.GetSubsectionAsync(sectionId, subsectionId, trackChanges: false);
            var articleDb = await _articleRepository.GetArticleAsync(subsectionId, id, trackChanges: false);
            if (articleDb == null)
            {
                return null;
            }
            var articleDto = _mapper.Map<ArticleDto>(articleDb);
            return articleDto;
        }
        public async Task<bool> CreateAricleForSubsection(Guid sectionId, Guid subsectionId, ArticleForCreationDto articleForCreationDto)
        {
            var subsection = await _subsectionRepository.GetSubsectionAsync(sectionId, subsectionId, trackChanges: false);
            if (subsection == null)
            {
                return false;
            }
            var subsectionEntity = _mapper.Map<Article>(articleForCreationDto);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Article, ArticleForCreationDto>()).CreateMapper();
            _articleRepository.CreateArticlesForSubsection(sectionId, subsectionId, subsectionEntity);
            return true;
        }
        public async Task<bool> DeleteAricleForSubsection(Guid sectionId, Guid subsectionId, Guid id)
        {
            var subSection = await _subsectionRepository.GetSubsectionAsync(sectionId, subsectionId, trackChanges: false);
            var articleForSubsection = await _articleRepository.GetArticleAsync(subsectionId, id, trackChanges: false);
            if (articleForSubsection == null)
            {
                return false;
            }
            _articleRepository.DeleteArticle(articleForSubsection);
            return true;
        }
        public async Task<bool> UpdateArticleForSubsection(Guid sectionId, Guid subsectionId, Guid id, ArticleForUpdateDto articleForCreationDto)
        {
            var subsection = await _subsectionRepository.GetSubsectionAsync(sectionId, subsectionId, trackChanges: false);
            var articleEntity = await _articleRepository.GetArticleAsync(subsectionId, id, trackChanges: true);
            if (articleEntity == null)
            {
                return false;
            }
            _mapper.Map(articleForCreationDto, articleEntity);
            return true;
        }
    }
}
