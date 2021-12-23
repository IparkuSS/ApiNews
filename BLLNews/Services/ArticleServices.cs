using AutoMapper;
using BLLNews.DataTransferObjects.ArticlesDto;
using BLLNews.Interfaces;
using DALNews.Models;
using DALNews.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BLLNews.Services
{
    /// <summary>
    /// the service class that serves the articles controller
    /// </summary>
    public class ArticleServices : IArticleServices
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public ArticleServices(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ArticleDto>> GetAriclesForSubsection(Guid sectionId, Guid subsectionId)
        {
            var article = await _repository.Subsection.GetSubsectionAsync(sectionId, subsectionId, trackChanges: false);
            if (article == null)
            {
                return null;
            }
            var articleFromDb = await _repository.Article.GetArticlesAsync(sectionId, subsectionId, trackChanges: false);
            var articleDto = _mapper.Map<IEnumerable<ArticleDto>>(articleFromDb);
            return articleDto;
        }
        public async Task<ArticleDto> GetAricleForSubsection(Guid sectionId, Guid subsectionId, Guid id)
        {
            var article = await _repository.Subsection.GetSubsectionAsync(sectionId, subsectionId, trackChanges: false);
            var articleDb = await _repository.Article.GetArticleAsync(subsectionId, id, trackChanges: false);
            if (articleDb == null)
            {
                return null;
            }
            var articleDto = _mapper.Map<ArticleDto>(articleDb);
            return articleDto;
        }
        public async Task<string> CreateAricleForSubsection(Guid sectionId, Guid subsectionId, ArticleForCreationDto articleForCreationDto)
        {
            var subsection = await _repository.Subsection.GetSubsectionAsync(sectionId, subsectionId, trackChanges: false);
            if (subsection == null)
            {
                return null;
            }
            var subsectionEntity = _mapper.Map<Article>(articleForCreationDto);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Article, ArticleForCreationDto>()).CreateMapper();
            _repository.Article.CreateArticlesForSubsection(sectionId, subsectionId, subsectionEntity);
            _repository.Article.SaveArticle();
            return "Ok";
        }
        public async Task<string> DeleteAricleForSubsection(Guid sectionId, Guid subsectionId, Guid id)
        {
            var subSection = await _repository.Subsection.GetSubsectionAsync(sectionId, subsectionId, trackChanges: false);
            var articleForSubsection = await _repository.Article.GetArticleAsync(subsectionId, id, trackChanges: false);
            if (articleForSubsection == null)
            {
                return null;
            }
            _repository.Article.DeleteArticle(articleForSubsection);
            _repository.Article.SaveArticle();
            return "Ok";
        }
        public async Task<string> UpdateArticleForSubsection(Guid sectionId, Guid subsectionId, Guid id, ArticleForUpdateDto articleForCreationDto)
        {
            var subsection = await _repository.Subsection.GetSubsectionAsync(sectionId, subsectionId, trackChanges: false);
            var articleEntity = await _repository.Article.GetArticleAsync(subsectionId, id, trackChanges: true);
            if (articleEntity == null)
            {
                return null;
            }
            _mapper.Map(articleForCreationDto, articleEntity);
            _repository.Article.SaveArticle();
            return "Ok";
        }
    }
}
