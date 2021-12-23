using BLLNews.DataTransferObjects.SubsectionsDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BLLNews.Interfaces
{
    public interface ISubsectionServices
    {
        Task<IEnumerable<SubsectionDto>> GetSubsectionsForSection(Guid sectionId);
        Task<SubsectionDto> GetSubsectionForSection(Guid id, Guid sectionId);
        Task<string> CreateSubsectionForSection(Guid sectionId, SubsectionForCreationDto subsectionForCreationDto);
        Task<string> DeleteSubsectionForSection(Guid id, Guid sectionId);
        Task<string> UpdateSubsectionForSection(Guid id, Guid sectionId, SubsectionForUpdateDto subsectionForUpdateDto);
    }
}
