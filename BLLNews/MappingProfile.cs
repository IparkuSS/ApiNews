using AutoMapper;
using News.BLL.DataTransferObjects.ArticlesDto;
using News.BLL.DataTransferObjects.AuthorsDto;
using News.BLL.DataTransferObjects.SectionsDto;
using News.BLL.DataTransferObjects.SubsectionsDto;
using News.BLL.DataTransferObjects.UserDto;
using News.DAL.Models;
namespace News.BLL.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Section, SectionDto>();
            CreateMap<Subsection, SubsectionDto>();
            CreateMap<Article, ArticleDto>();
            CreateMap<AuthorCreatDto, Author>();
            CreateMap<Author, AuthorDto>();
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<SectionForCreationDto, Section>();
            CreateMap<SubsectionForCreationDto, Subsection>();
            CreateMap<SubsectionForUpdateDto, Subsection>();
            CreateMap<ArticleForCreationDto, Article>();
            CreateMap<SectionForUpdateDto, Section>();
            CreateMap<ArticleForUpdateDto, Article>();
        }
    }
}
