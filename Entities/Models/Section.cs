using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Section
    {
        [Column("IdSection")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Название секции не должно отсутствовать")]
        [MaxLength(15, ErrorMessage = "Максимальный размер 15 букв")]
        public string NameSection { get; set; }

        [Required(ErrorMessage = "Картинка не должна отсутствовать")]
        public string TitleImagePath { get; set; }
        public ICollection<Subsection> Subsections { get; set; }
    }
}
