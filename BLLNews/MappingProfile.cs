using AutoMapper;
using BLLNews.DataTransferObjects.ArticlesDto;
using BLLNews.DataTransferObjects.AuthorsDto;
using BLLNews.DataTransferObjects.SectionsDto;
using BLLNews.DataTransferObjects.SubsectionsDto;
using BLLNews.DataTransferObjects.UserDto;
using DALNews.Models;
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
