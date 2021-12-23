using Microsoft.AspNetCore.Identity;
namespace DALNews.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
