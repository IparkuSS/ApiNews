using APINews.Controllers;
using APINews.Extensions;
using AutoFixture;
using AutoMapper;
using Contract;
using Contract.Repositories;
using Entities.DataTransferObjects.SectionsDto;
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
    public class SectionControllerTests
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapperFake;
        private readonly Fixture _fixture;
        private readonly SectionController _controller;
        public SectionControllerTests()
        {
            _repositoryManager = A.Fake<IRepositoryManager>();
            _loggerManager = A.Fake<ILoggerManager>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapperFake = config.CreateMapper();
            _controller = new SectionController(_repositoryManager, _loggerManager, _mapperFake);
            _fixture = new Fixture();
        }
        [Fact]
        public async Task GetAllSectionTests_ShouldReturnActionResultOfSectionsWith200StatusCode()
        {
            //Arange 
            var sections = _fixture.CreateMany<Section>(3).ToList();
            A.CallTo(() => _repositoryManager.Section.GetAllSectionAsync(false)).Returns(sections);
            //Act
            var result = await _controller.GetAllSection() as OkObjectResult;
            //Assert
            A.CallTo(() => _repositoryManager.Section.GetAllSectionAsync(false)).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            var returnValue = result.Value as IEnumerable<SectionDto>;
            Assert.Equal(sections.Count, returnValue.Count());
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        [Fact]
        public async Task GetSectionTests_ShouldReturnActionResultOfSectionWith200StatusCode()
        {
            //Arange 
            var sections = _fixture.CreateMany<Section>(3).ToList();
            var section = sections.FirstOrDefault();
            A.CallTo(() => _repositoryManager.Section.GetSectionAsync(section.Id, false)).Returns(section);
            // Act
            var result = await _controller.GetSection(section.Id) as OkObjectResult;
            // Assert
            A.CallTo(() => _repositoryManager.Section.GetSectionAsync(section.Id, false)).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            var returnValue = result.Value as SectionDto;
            Assert.Equal(section.Id, returnValue.Id);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task PostSectionTests_ShouldReturn204StatusCode()
        {
            //Arange 
            var sections = _fixture.CreateMany<SectionForCreationDto>(3).ToList();
            var section = sections.FirstOrDefault();
            // Act
            var result = await _controller.CreateSection(section) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task UpdateSectionTests_ShouldReturn204StatusCode()
        {
            //Arange 
            var sectionsDto = _fixture.CreateMany<SectionForUpdateDto>(3).ToList();
            var sections = _fixture.CreateMany<Section>(3).ToList();
            // Act
            var result = await _controller.UpdateSection(sections.Select(p => p.Id)
                .FirstOrDefault(), sectionsDto.FirstOrDefault()) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task DeleteSectionTests_ShouldReturn204StatusCode()
        {
            //Arange 
            var sections = _fixture.CreateMany<Section>(3).ToList();
            // Act
            var result = await _controller.DeleteSection(sections.Select(p => p.Id).FirstOrDefault()) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
