using BLLNews.DataTransferObjects.SectionsDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BLLNews.Interfaces
{
    public interface ISectionServices
    {
        Task<IEnumerable<SectionDto>> GetSections();
        Task<SectionDto> GetSection(Guid sectionId);
        Task<bool> CreateSection(SectionForCreationDto sectionForCreationDto);
        Task<string> DeleteSection(Guid sectionId);
        Task<string> UpdateSection(Guid sectionId, SectionForUpdateDto sectionForUpdateDto);
    }
}
