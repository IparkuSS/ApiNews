using System.ComponentModel.DataAnnotations;
namespace News.MVC.Models
{
    public class UserForAuthentication
    {
        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }
        [Required]
        [UIHint("password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
