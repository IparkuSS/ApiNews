using System;
namespace News.BLL.DataTransferObjects.SubsectionsDto
{
    public class SubsectionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid IdSection { get; set; }

        public string ImagePath { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }
    }
}
