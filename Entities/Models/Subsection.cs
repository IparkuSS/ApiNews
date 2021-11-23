using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Subsection
    {
        [Column("IdSubsection")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Название подсекции не должно отсутствовать")]
        [MaxLength(15, ErrorMessage = "Максимальный размер 15 букв")]
        public string Name { get; set; }

        [ForeignKey(nameof(Section))]
        public Guid IdSection { get; set; }
        [Required(ErrorMessage = "Картинка не должна отсутствовать")]
        public string ImagePath { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
