using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace News.DAL.Models
{
    public class Subsection
    {
        [Column("IdSubsection")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Subsection name must not be missing")]
        [MaxLength(15, ErrorMessage = "Maximum size 15 letters")]
        public string Name { get; set; }

        [ForeignKey(nameof(Section))]
        public Guid IdSection { get; set; }

        [Required(ErrorMessage = "Picture should not be missing")]
        public string ImagePath { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
