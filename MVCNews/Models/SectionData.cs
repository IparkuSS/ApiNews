using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace News.MVC.Models
{
    public class SectionData
    {
        [Column("IdSection")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Имя раздела не должно отсутствовать")]
        [MaxLength(15, ErrorMessage = "Максимальный размер 15 букв")]
        public string NameSection { get; set; }
        [Required(ErrorMessage = "Картинка не должна отсутствовать")]
        public string TitleImagePath { get; set; }
    }
}
