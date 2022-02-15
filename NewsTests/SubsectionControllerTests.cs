using AutoFixture;
using AutoMapper;
using Contract;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.API.Controllers;
using News.BLL.DataTransferObjects.SubsectionsDto;
using News.BLL.Extensions;
using News.BLL.Interfaces;
using News.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace News.Tests
{
    public class SubsectionControllerTests
    {
        private readonly ISubsectionServices _subsectionManager;
        private readonly ILoggerManager _loggerManager;
        private readonly Fixture _fixture;
        private readonly SubsectionController _controller;
        public SubsectionControllerTests()
        {
            _subsectionManager = A.Fake<ISubsectionServices>();
            _loggerManager = A.Fake<ILoggerManager>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _controller = new SubsectionController(_loggerManager, _subsectionManager);
            _fixture = new Fixture();
        }
        /* [Fact]
         public async Task GetAllSubsectionTests_ShouldReturnActionResultOfSubsectionsWith200StatusCode()
         {
             //Arange 
             var subsection = _fixture.CreateMany<SubsectionDto>(3).ToList();
             var sectionId = subsection.Select(p => p.IdSection).FirstOrDefault();
             A.CallTo(() => _subsectionManager.GetSubsectionsForSection(sectionId)).Returns(subsection);
             //Act
             var result = await _controller.GetSubsectionsForSection(sectionId) as OkObjectResult;
             //Assert
             A.CallTo(() => _subsectionManager.GetSubsectionsForSection(sectionId)).MustHaveHappenedOnceExactly();
             Assert.NotNull(result);
             var returnValue = result.Value as IEnumerable<SubsectionDto>;
             Assert.Equal(subsection.Count, returnValue.Count());
             Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
         }*/
        [Fact]
        public async Task GetSubsectionTests_ShouldReturnActionResultOfSubsectionWith200StatusCode()
        {
            //Arange 
            var subsections = _fixture.CreateMany<SubsectionDto>(3).ToList();
            var subsection = subsections.FirstOrDefault();
            A.CallTo(() => _subsectionManager.GetSubsectionForSection(subsection.Id, subsection.IdSection)).Returns(subsection);
            // Act
            var result = await _controller.GetSubsectionForSection(subsection.IdSection, subsection.Id) as OkObjectResult;
            // Assert
            A.CallTo(() => _subsectionManager.GetSubsectionForSection(subsection.Id, subsection.IdSection))
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
