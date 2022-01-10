using AutoFixture;
using AutoMapper;
using Contract;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.API.Controllers;
using News.BLL.DataTransferObjects.SectionsDto;
using News.BLL.Extensions;
using News.BLL.Interfaces;
using News.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace News.Tests
{
    public class SectionControllerTests
    {
        private readonly ISectionServices _sectionManager;
        private readonly ILoggerManager _loggerManager;
        private readonly Fixture _fixture;
        private readonly SectionController _controller;
        public SectionControllerTests()
        {
            _sectionManager = A.Fake<ISectionServices>();
            _loggerManager = A.Fake<ILoggerManager>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _controller = new SectionController(_loggerManager, _sectionManager);
            _fixture = new Fixture();
        }
        [Fact]
        public async Task GetAllSectionTests_ShouldReturnActionResultOfSectionsWith200StatusCode()
        {
            //Arange 
            var sections = _fixture.CreateMany<SectionDto>(3).ToList();
            A.CallTo(() => _sectionManager.GetSections()).Returns(sections);
            //Act
            var result = await _controller.GetAllSection() as OkObjectResult;
            //Assert
            A.CallTo(() => _sectionManager.GetSections()).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            var returnValue = result.Value as IEnumerable<SectionDto>;
            Assert.Equal(sections.Count, returnValue.Count());
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        [Fact]
        public async Task GetSectionTests_ShouldReturnActionResultOfSectionWith200StatusCode()
        {
            //Arange 
            var sections = _fixture.CreateMany<SectionDto>(3).ToList();
            var section = sections.FirstOrDefault();
            A.CallTo(() => _sectionManager.GetSection(section.Id)).Returns(section);
            // Act
            var result = await _controller.GetSection(section.Id) as OkObjectResult;
            // Assert
            A.CallTo(() => _sectionManager.GetSection(section.Id)).MustHaveHappenedOnceExactly();
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
