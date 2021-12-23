using System;
namespace BLLNews.DataTransferObjects.ArticlesDto
{
    public class ArticleForCreationDto
    {
        public string ShortDescription { get; set; }
        public string Heading { get; set; }
        public int Priority { get; set; }
        public string Image { get; set; }
        public DateTime AddTime { get; set; }
        public string Text { get; set; }
    }
}
