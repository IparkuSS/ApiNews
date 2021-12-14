using Entities.Models;
using System.Collections.Generic;
namespace Entities.DataTransferObjects.SectionsDto
{
    public class SectionForUpdateDto
    {
        public string NameSection { get; set; }
        public string TitleImagePath { get; set; }
        public ICollection<Subsection> Subsections { get; set; }
    }
}
