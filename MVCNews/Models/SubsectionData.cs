using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCNews.Models
{
    public class SubsectionData
    {

        [Column("IdSubsection")]
        public Guid Id { get; set; }
        /* [Required(ErrorMessage = "Название подсекции не должно отсутствовать")]
         [MaxLength(15, ErrorMessage = "Максимальный размер 15 букв")]*/
        public string Name { get; set; }

        public Guid IdSection { get; set; }
        [Required(ErrorMessage = "Картинка не должна отсутствовать")]
        public string ImagePath { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }


    }
}
