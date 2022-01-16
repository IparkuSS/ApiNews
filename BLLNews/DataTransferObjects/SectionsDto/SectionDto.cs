using System;
namespace News.BLL.DataTransferObjects.SectionsDto
{
    public class SectionDto
    {
        public Guid Id { get; set; }

        public string NameSection { get; set; }

        public string TitleImagePath { get; set; }
    }
}
