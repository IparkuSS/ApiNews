using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.DAL.Models;
using System;
namespace News.DAL.Configuration
{
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.HasData(
                new Section
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    NameSection = "Sport",
                    TitleImagePath = "Sport.jpg"
                },
                new Section
                {
                    Id = new Guid("38736ae1-08a1-4d45-9c0e-b0b3711f92e7"),
                    NameSection = "World",
                    TitleImagePath = "USA.jpg"
                },
                new Section
                {
                    Id = new Guid("9ea5b28f-9b62-49d2-853b-8f031bc51091"),
                    NameSection = "War",
                    TitleImagePath = "war.jpg"
                });
        }
    }
}
