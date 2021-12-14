using APINews.Controllers;
using APINews.Extensions;
using AutoFixture;
using AutoMapper;
using Contract;
using Contract.Repositories;
using Entities.DataTransferObjects.AuthorsDto;
using Entities.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
namespace NewsTests
{
    public class AuthorControllerTests
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapperFake;
        private readonly Fixture _fixture;
        private readonly AuthorController _controller;
        public AuthorControllerTests()
        {
            _repositoryManager = A.Fake<IRepositoryManager>();
            _loggerManager = A.Fake<ILoggerManager>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapperFake = config.CreateMapper();
            _controller = new AuthorController(_repositoryManager, _loggerManager, _mapperFake);
            _fixture = new Fixture();
        }
        [Fact]
        public async Task GetAllAuthorsTests_ShouldReturnActionResultOfAuthorsWith200StatusCode()
        {
            //Arange 
            var authors = _fixture.CreateMany<Author>(3).ToList();
            A.CallTo(() => _repositoryManager.Author.GetAllAuthorsAsync(false)).Returns(authors);
            //Act
            var result = await _controller.GetAuthors() as OkObjectResult;
            //Assert
            A.CallTo(() => _repositoryManager.Author.GetAllAuthorsAsync(false)).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            var returnValue = result.Value as IEnumerable<AuthorDto>;
            Assert.Equal(authors.Count, returnValue.Count());
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        [Fact]
        public async Task GetAuthorTests_ShouldReturnActionResultOfAuthorWith200StatusCode()
        {
            //Arange 
            var authors = _fixture.CreateMany<Author>(3).ToList();
            var author = authors.FirstOrDefault();
            A.CallTo(() => _repositoryManager.Author.GetAuthorAsync(author.Id, false)).Returns(author);
            // Act
            var result = await _controller.GetAuthor(author.Id) as OkObjectResult;
            // Assert
            A.CallTo(() => _repositoryManager.Author.GetAuthorAsync(author.Id, false)).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            var returnValue = result.Value as AuthorDto;
            Assert.Equal(author.Id, returnValue.Id);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task PostAuthorTests_ShouldReturn204StatusCode()
        {
            //Arange 
            var authors = _fixture.CreateMany<AuthorCreatDto>(3).ToList();
            var author = authors.FirstOrDefault();
            // Act
            var result = await _controller.CreateAuthor(author) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task DeleteAuthorTests_ShouldReturn204StatusCode()
        {
            //Arange 
            var authors = _fixture.CreateMany<Author>(3).ToList();
            // Act
            var result = await _controller.DeleteAuthor(authors.Select(p => p.Id).FirstOrDefault()) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
