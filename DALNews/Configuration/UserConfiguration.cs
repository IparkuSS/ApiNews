using News.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace News.DAL.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
               new User
               {
                   Id = "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                   UserName = "Ad",
                   NormalizedUserName = "ADMIN",
                   Email = "my@email.com",
                   NormalizedEmail = "MY@EMAIL.COM",
                   EmailConfirmed = true,
                   PasswordHash = new PasswordHasher<User>().HashPassword(null, "superpassword"),
                   SecurityStamp = string.Empty
               });
        }
    }
}
