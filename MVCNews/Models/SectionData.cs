using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCNews.Models
{
    public class SectionData
    {
        [Column("IdSection")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Название секции не должно отсутствовать")]
        [MaxLength(15, ErrorMessage = "Максимальный размер 15 букв")]
        public string NameSection { get; set; }


        public string TitleImagePath { get; set; }

    }
}
