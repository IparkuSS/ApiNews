using BLLNews.DataTransferObjects.ArticlesDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BLLNews.Interfaces
{
    public interface IArticleServices
    {
        Task<IEnumerable<ArticleDto>> GetAriclesForSubsection(Guid sectionId, Guid subsectionId);
        Task<ArticleDto> GetAricleForSubsection(Guid sectionId, Guid subsectionId, Guid id);
        Task<string> CreateAricleForSubsection(Guid sectionId, Guid subsectionId, ArticleForCreationDto articleForCreationDto);
        Task<string> DeleteAricleForSubsection(Guid sectionId, Guid subsectionId, Guid id);
        Task<string> UpdateArticleForSubsection(Guid sectionId, Guid subsectionId, Guid id, ArticleForUpdateDto articleForCreationDto);
    }
}
