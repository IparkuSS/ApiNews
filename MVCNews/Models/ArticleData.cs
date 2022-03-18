using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace News.MVC.Models
{
    public class ArticleData
    {
        [Column("IdArticle")]
        public Guid Id { get; set; }
        public Guid IdSubsection { get; set; }
        public Guid IdAuthor { get; set; }
        [Required(ErrorMessage = "Краткое описание отсутствует")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Заголовок не должен отсутствовать")]
        public string Heading { get; set; }
        [Required(ErrorMessage = "Приоритет не должен отсутствовать")]
        public int Priority { get; set; }
        [Required(ErrorMessage = "Картинка не должна отсуствавать")]
        public string Image { get; set; }
        public DateTime AddTime { get; set; }
        [Required(ErrorMessage = "Текст не должен отсуствавать")]
        public string Text { get; set; }
    }
}
