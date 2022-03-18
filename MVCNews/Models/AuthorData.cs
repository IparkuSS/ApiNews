using System;
using System.ComponentModel.DataAnnotations;
namespace News.MVC.Models
{
    public class AuthorData
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Заполните поле имени")]
        [Display(Name = "Ваша имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Заполните поле Фамилия")]
        [Display(Name = "Ваша фамилия")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Заполните поле файла")]
        [Display(Name = "Ваш файл")]
        public string Document { get; set; }
    }
}
