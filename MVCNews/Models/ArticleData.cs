using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCNews.Models
{
    public class ArticleData
    {
        [Column("IdArticle")]
        public Guid Id { get; set; }

        public Guid IdSubsection { get; set; }

        public Guid IdAuthor { get; set; }
        [Required(ErrorMessage = "Краткое описание не должно отсутствовать")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "заголовок не должен отсутствовать")]
        public string Heading { get; set; }
        [Required(ErrorMessage = "Приормтет не должен отсутствовать")]
        public int Priority { get; set; }
        public string Image { get; set; }
        public DateTime AddTime { get; set; }
        [Required(ErrorMessage = "Текст не должен отсутствовать")]
        public string Text { get; set; }
    }
}
