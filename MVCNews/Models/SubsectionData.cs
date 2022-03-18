using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace News.MVC.Models
{
    public class SubsectionData
    {
        [Column("IdSubsection")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Название подраздела не должно отсутствовать")]
        [MaxLength(15, ErrorMessage = "Максимальный размер 15 букв")]
        public string Name { get; set; }
        public Guid IdSection { get; set; }
        [Required(ErrorMessage = "Картинка не должна отсутствовать")]
        public string ImagePath { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
    }
}
