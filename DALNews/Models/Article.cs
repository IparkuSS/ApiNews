using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace News.DAL.Models
{
    public class Article
    {
        [Column("IdArticle")]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Subsection))]
        public Guid IdSubsection { get; set; }
        [ForeignKey(nameof(Author))]
        public Guid IdAuthor { get; set; }
        [Required(ErrorMessage = "Short description not missing")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Heading should not be missing")]
        public string Heading { get; set; }
        [Required(ErrorMessage = "Priority should not be missing")]
        public int Priority { get; set; }
        public string Image { get; set; }
        public DateTime AddTime { get; set; }
        [Required(ErrorMessage = "Text should not be missing")]
        public string Text { get; set; }
    }
}
