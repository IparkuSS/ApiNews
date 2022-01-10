using News.BLL.DataTransferObjects.SubsectionsDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace News.BLL.Interfaces
{
    public interface ISubsectionServices
    {
        Task<IEnumerable<SubsectionDto>> GetSubsectionsForSection(Guid sectionId);

        Task<SubsectionDto> GetSubsectionForSection(Guid id, Guid sectionId);

        Task<bool> CreateSubsectionForSection(Guid sectionId, SubsectionForCreationDto subsectionForCreationDto);

        Task<bool> DeleteSubsectionForSection(Guid id, Guid sectionId);

        Task<bool> UpdateSubsectionForSection(Guid id, Guid sectionId, SubsectionForUpdateDto subsectionForUpdateDto);
    }
}
