using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    internal class ArtistConfig
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            // Required field bandName
            builder
                .Property(a => a.bandName)
                .IsRequired()
                .HasMaxLength(100);

            // Uniqueness of bandName
            builder
                .HasIndex(a => a.bandName)
                .IsUnique();

            // Cascading delete: if an Artist is deleted, all related Albums are deleted
            builder
                .HasMany(a => a.Albums)
                .WithOne(a => a.Artist)
                .HasForeignKey(a => a.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-Many relationship with Genre via ArtistGenre
            builder
                .HasMany(a => a.Genres)
                .WithMany(g => g.Artists)
                .UsingEntity("ArtistGenre");

            // Connections Artist-Genre
            builder.HasData(
                new { ArtistsArtistId = 1, GenresGenreId = 1 }, // Three Days Grace - Post-Grunge
                new { ArtistsArtistId = 1, GenresGenreId = 2 }, // Three Days Grace - Alternative Metal
                new { ArtistsArtistId = 2, GenresGenreId = 3 }, // Metallica - Thrash Metal
                new { ArtistsArtistId = 2, GenresGenreId = 4 }, // Metallica - Heavy Metal
                new { ArtistsArtistId = 3, GenresGenreId = 4 }, // Disturbed - Heavy Metal
                new { ArtistsArtistId = 3, GenresGenreId = 5 }  // Disturbed - Nu Metal
            );

            // Artists
            builder.HasData(
                new Artist
                {
                    ArtistId = 1,
                    bandName = "Three Days Grace",
                    Country = "Canada",
                    yearsOfActivity = "1997-present",
                    Biography = "Canadian rock band formed in Norwood, Ontario."
                },
                new Artist
                {
                    ArtistId = 2,
                    bandName = "Metallica",
                    Country = "USA",
                    yearsOfActivity = "1981-present",
                    Biography = "American heavy metal band, one of the 'Big Four' of thrash metal."
                },
                new Artist
                {
                    ArtistId = 3,
                    bandName = "Disturbed",
                    Country = "USA",
                    yearsOfActivity = "1994-present",
                    Biography = "American heavy metal band from Chicago."
                }
            );

        }
    }
}
