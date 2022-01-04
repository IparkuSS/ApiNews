using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace News.DAL.Models
{
    public class Author
    {
        [Column("IdAuthor")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Document { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
