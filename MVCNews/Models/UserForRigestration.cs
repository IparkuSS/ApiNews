using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace News.MVC.Models
{
    public class UserForRigestration
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "поле Username Должно быть заполненно")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "поле Password Должно быть заполненно")]
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
