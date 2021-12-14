using APINews.Controllers;
using APINews.Extensions;
using AutoFixture;
using AutoMapper;
using Contract;
using Contract.Repositories;
using Entities.DataTransferObjects.SubsectionsDto;
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
    public class SubsectionControllerTests
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapperFake;
        private readonly Fixture _fixture;
        private readonly SubsectionController _controller;
        public SubsectionControllerTests()
        {
            _repositoryManager = A.Fake<IRepositoryManager>();
            _loggerManager = A.Fake<ILoggerManager>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapperFake = config.CreateMapper();
            _controller = new SubsectionController(_repositoryManager, _loggerManager, _mapperFake);
            _fixture = new Fixture();
        }
        [Fact]
        public async Task GetAllSubsectionTests_ShouldReturnActionResultOfSubsectionsWith200StatusCode()
        {
            //Arange 
            var subsection = _fixture.CreateMany<Subsection>(3).ToList();
            var sectionId = subsection.Select(p => p.IdSection).FirstOrDefault();
            A.CallTo(() => _repositoryManager.Subsection.GetSubsectionsAsync(sectionId, false)).Returns(subsection);
            //Act
            var result = await _controller.GetSubsectionsForSection(sectionId) as OkObjectResult;
            //Assert
            A.CallTo(() => _repositoryManager.Subsection.GetSubsectionsAsync(sectionId, false)).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            var returnValue = result.Value as IEnumerable<SubsectionDto>;
            Assert.Equal(subsection.Count, returnValue.Count());
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        [Fact]
        public async Task GetSubsectionTests_ShouldReturnActionResultOfSubsectionWith200StatusCode()
        {
            //Arange 
            var subsections = _fixture.CreateMany<Subsection>(3).ToList();
            var subsection = subsections.FirstOrDefault();
            A.CallTo(() => _repositoryManager.Subsection.GetSubsectionAsync(subsection.IdSection, subsection.Id, false)).Returns(subsection);
            // Act
            var result = await _controller.GetSubsectionForSection(subsection.IdSection, subsection.Id) as OkObjectResult;
            // Assert
            A.CallTo(() => _repositoryManager.Subsection.GetSubsectionAsync(subsection.IdSection, subsection.Id, false))
                .MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            var returnValue = result.Value as SubsectionDto;
            Assert.Equal(subsection.Id, returnValue.Id);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateSubsectionTests_ShouldReturn204StatusCode()
        {
            //Arange 
            var subsectionDto = _fixture.CreateMany<SubsectionForUpdateDto>(3).ToList();
            var subsections = _fixture.CreateMany<Subsection>(3).ToList();
            // Act
            var result = await _controller.UpdateSubsectionForSection(subsections.Select(p => p.IdSection)
                .FirstOrDefault(), subsections.Select(p => p.Id)
                .FirstOrDefault(), subsectionDto.FirstOrDefault()) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task DeleteSubsectionTests_ShouldReturn204StatusCode()
        {
            //Arange 
            var subsections = _fixture.CreateMany<Subsection>(3).ToList();
            // Act
            var result = await _controller.DeleteSubsectionForSection(subsections.Select(p => p.IdSection)
                .FirstOrDefault(), subsections.Select(p => p.Id).FirstOrDefault()) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
