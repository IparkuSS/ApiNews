using System;
using System.ComponentModel.DataAnnotations;
namespace MVCNews.Models
{
    public class AuthorData
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Fill in the name field")]
        [Display(Name = "Your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Fill in the surname field")]
        [Display(Name = "Your surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Fill in the document field")]
        [Display(Name = "Your document")]
        public string Document { get; set; }
    }
}
