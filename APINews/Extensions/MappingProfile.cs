using AutoMapper;
using Entities.DataTransferObjects.ArticlesDto;
using Entities.DataTransferObjects.AuthorsDto;
using Entities.DataTransferObjects.SectionsDto;
using Entities.DataTransferObjects.SubsectionsDto;
using Entities.DataTransferObjects.UserDto;
using Entities.Models;
namespace APINews.Extensions
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
