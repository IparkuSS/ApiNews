using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVCNews.Models
{
    public class SectionData
    {
        [Column("IdSection")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Section name should not be missing")]
        [MaxLength(15, ErrorMessage = "Maximum size 15 letters")]
        public string NameSection { get; set; }
        [Required(ErrorMessage = "The picture should not be missing")]
        public string TitleImagePath { get; set; }
    }
}
