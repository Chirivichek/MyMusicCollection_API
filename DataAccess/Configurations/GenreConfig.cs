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
        }
    }
}