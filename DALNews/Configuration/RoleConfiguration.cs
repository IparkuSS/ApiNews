using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DALNews.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "44546e06-8719-4ad8-b88a-f271ae9d6eab",
                    Name = "Redactor",
                    NormalizedName = "REDACTOR"
                },
                new IdentityRole
                {
                    Id = "4f269b9e-308f-4467-87e3-2a39a48d25fd",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                });
        }
    }
}
