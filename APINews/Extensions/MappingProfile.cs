using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CreateMap<AuthorDto, Author>();
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
