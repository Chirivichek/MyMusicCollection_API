using System.Reflection.Emit;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    internal class GenreConfig : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            // Required field GenreName
            builder
                .Property(g => g.GenreName)
                .IsRequired()
                .HasMaxLength(50);

            // Uniqueness of GenreName
            builder
                .HasIndex(g => g.GenreName)
                .IsUnique();


            //Ganres
            builder.HasData(
                new Genre { GenreId = 1, GenreName = "Post-Grunge" },
                new Genre { GenreId = 2, GenreName = "Alternative Metal" },
                new Genre { GenreId = 3, GenreName = "Thrash Metal" },
                new Genre { GenreId = 4, GenreName = "Heavy Metal" },
                new Genre { GenreId = 5, GenreName = "Nu Metal" }
            );
        }
    }
}