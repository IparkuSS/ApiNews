using News.BLL.DataTransferObjects.SectionsDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace News.BLL.Interfaces
{
    public interface ISectionServices
    {
        Task<IEnumerable<SectionDto>> GetSections();
        Task<SectionDto> GetSection(Guid sectionId);
        Task<bool> CreateSection(SectionForCreationDto sectionForCreationDto);
        Task<bool> DeleteSection(Guid sectionId);
        Task<bool> UpdateSection(Guid sectionId, SectionForUpdateDto sectionForUpdateDto);
    }
}
