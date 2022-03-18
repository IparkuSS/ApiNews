using News.BLL.DataTransferObjects.ArticlesDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using News.DAL.Parameters;

namespace News.BLL.Interfaces
{
    public interface IArticleServices
    {
        Task<IEnumerable<ArticleDto>> GetAriclesForSubsection(Guid sectionId, Guid subsectionId, ArticlesParameters articlesParameters);

        Task<ArticleDto> GetAricleForSubsection(Guid sectionId, Guid subsectionId, Guid id);

        Task<bool> CreateAricleForSubsection(Guid sectionId, Guid subsectionId, ArticleForCreationDto articleForCreationDto);

        Task<bool> DeleteAricleForSubsection(Guid sectionId, Guid subsectionId, Guid id);

        Task<bool> UpdateArticleForSubsection(Guid sectionId, Guid subsectionId, Guid id, ArticleForUpdateDto articleForCreationDto);
    }
}
