using APINews.Controllers;
using APINews.Extensions;
using AutoFixture;
using AutoMapper;
using Contract;
using Contract.Repositories;
using Entities.DataTransferObjects.ArticlesDto;
using Entities.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
namespace NewsTests
{
    public class ArticleControllerTests
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapperFake;
        private readonly Fixture _fixture;
        private readonly ArticleController _controller;
        public ArticleControllerTests()
        {
            _repositoryManager = A.Fake<IRepositoryManager>();
            _loggerManager = A.Fake<ILoggerManager>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapperFake = config.CreateMapper();
            _controller = new ArticleController(_repositoryManager, _loggerManager, _mapperFake);
            _fixture = new Fixture();
        }
        [Fact]
        public async Task GetAllSArticlesTests_ShouldReturnActionResultOfArticlesWith200StatusCode()
        {
            //Arange 
            var fakeSectionGuid = Guid.NewGuid();
            var articles = _fixture.CreateMany<Article>(3).ToList();
            var article = articles.FirstOrDefault();
            A.CallTo(() => _repositoryManager.Article.GetArticlesAsync(fakeSectionGuid, article.IdSubsection, false)).Returns(articles);
            //Act
            var result = await _controller.GetAriclesForSubsection(fakeSectionGuid, article.IdSubsection) as OkObjectResult;
            //Assert
            A.CallTo(() => _repositoryManager.Article.GetArticlesAsync(fakeSectionGuid, article.IdSubsection, false)).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            var returnValue = result.Value as IEnumerable<ArticleDto>;
            Assert.Equal(articles.Count, returnValue.Count());
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        [Fact]
        public async Task GetArticleTests_ShouldReturnActionResultOfArticleWith200StatusCode()
        {
            //Arange 
            var fakeSectionGuid = Guid.NewGuid();
            var articles = _fixture.CreateMany<Article>(3).ToList();
            var article = articles.FirstOrDefault();
            A.CallTo(() => _repositoryManager.Article.GetArticleAsync(article.IdSubsection, article.Id, false)).Returns(article);
            // Act
            var result = await _controller.GetArticleForSubsection(fakeSectionGuid, article.IdSubsection, article.Id) as OkObjectResult;
            // Assert
            A.CallTo(() => _repositoryManager.Article.GetArticleAsync(article.IdSubsection, article.Id, false)).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            var returnValue = result.Value as ArticleDto;
            Assert.Equal(article.Id, returnValue.Id);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task PostArticleTests_ShouldReturn204StatusCode()
        {
            //Arange 
            var fakeSectionGuid = Guid.NewGuid();
            var fakeSubsectionGuid = Guid.NewGuid();
            var articles = _fixture.CreateMany<ArticleForCreationDto>(3).ToList();
            var article = articles.FirstOrDefault();
            // Act
            var result = await _controller.CreateArticleForSubsection(fakeSectionGuid, fakeSubsectionGuid, article) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task UpdaterticleTests_ShouldReturn204StatusCode()
        {
            //Arange 
            var fakeSectionGuid = Guid.NewGuid();
            var fakeSubsectionGuid = Guid.NewGuid();
            var fakeArticleGuid = Guid.NewGuid();
            var articlesDto = _fixture.CreateMany<ArticleForUpdateDto>(3).ToList();
            // Act
            var result = await _controller.UpdateArticleForSubsection(fakeSectionGuid, fakeSubsectionGuid, fakeArticleGuid,
                articlesDto.FirstOrDefault()) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task DeleteArticleTests_ShouldReturn204StatusCode()
        {
            //Arange 
            var articles = _fixture.CreateMany<Article>(3).ToList();
            var article = articles.FirstOrDefault();
            var fakeSectionGuid = Guid.NewGuid();
            // Act
            var result = await _controller.DeleteArticleForSubsection(fakeSectionGuid, article.IdSubsection, article.Id) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
