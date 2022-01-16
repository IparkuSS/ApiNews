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
        [Required(ErrorMessage = "Short description not missing")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Heading should not be missing")]
        public string Heading { get; set; }
        [Required(ErrorMessage = "Priority should not be missing")]
        public int Priority { get; set; }
        public string Image { get; set; }
        public DateTime AddTime { get; set; }
        public string Text { get; set; }
    }
}
