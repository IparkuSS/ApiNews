using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Author
    {
        [Column("IdAuthor")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required(ErrorMessage = "Документ не должен отсутствовать")]
        public string Document { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
