using Entities.Models;
using System;
using System.Collections.Generic;
namespace Entities.DataTransferObjects.SectionsDto
{
    public class SectionDto
    {
        public Guid Id { get; set; }
        public string NameSection { get; set; }
        public string TitleImagePath { get; set; }
        public ICollection<Subsection> Subsections { get; set; }
    }
}
