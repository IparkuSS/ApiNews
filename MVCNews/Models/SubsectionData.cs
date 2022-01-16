using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace News.MVC.Models
{
    public class SubsectionData
    {
        [Column("IdSubsection")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Subsection name must not be missing")]
        [MaxLength(15, ErrorMessage = "Maximum size 15 letters")]
        public string Name { get; set; }
        public Guid IdSection { get; set; }
        [Required(ErrorMessage = "Picture should not be missing")]
        public string ImagePath { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
    }
}
