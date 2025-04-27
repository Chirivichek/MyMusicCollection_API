using System.Reflection.Emit;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    internal class AlbumConfig : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {

            builder.HasData(
                // Three Days Grace
                new Album
                {
                    AlbumId = 1,
                    AlbumName = "Three Days Grace",
                    AlbumDuration = 934, // 258+192+229+255
                    TrackCount = 4,
                    Label = "Jive Records",
                    Format = "CD",
                    ReleaseDate = new DateTime(2003, 7, 22),
                    ArtistId = 1
                },
                new Album
                {
                    AlbumId = 2,
                    AlbumName = "One-X",
                    AlbumDuration = 855, // 231+209+208+207
                    TrackCount = 4,
                    Label = "Jive Records",
                    Format = "CD",
                    ReleaseDate = new DateTime(2006, 6, 13),
                    ArtistId = 1
                },
                // Metallica
                new Album
                {
                    AlbumId = 3,
                    AlbumName = "Master of Puppets",
                    AlbumDuration = 1710, // 312+515+387+496
                    TrackCount = 4,
                    Label = "Elektra Records",
                    Format = "Vinyl",
                    ReleaseDate = new DateTime(1986, 3, 3),
                    ArtistId = 2
                },
                new Album
                {
                    AlbumId = 4,
                    AlbumName = "Metallica",
                    AlbumDuration = 1429, // 331+324+386+388
                    TrackCount = 4,
                    Label = "Elektra Records",
                    Format = "CD",
                    ReleaseDate = new DateTime(1991, 8, 12),
                    ArtistId = 2
                },
                // Disturbed
                new Album
                {
                    AlbumId = 5,
                    AlbumName = "Ten Thousand Fists",
                    AlbumDuration = 1029, // 214+245+283+287
                    TrackCount = 4,
                    Label = "Reprise Records",
                    Format = "CD",
                    ReleaseDate = new DateTime(2005, 9, 20),
                    ArtistId = 3
                },
                new Album
                {
                    AlbumId = 6,
                    AlbumName = "Immortalized",
                    AlbumDuration = 988, // 253+263+245+227
                    TrackCount = 4,
                    Label = "Reprise Records",
                    Format = "Digital",
                    ReleaseDate = new DateTime(2015, 8, 21),
                    ArtistId = 3
                }
            );


            // Required field AlbumName
            builder
                .Property(a => a.AlbumName)
                .IsRequired()
                .HasMaxLength(100);

            // Uniqueness of AlbumName for one Artist
            builder
                .HasIndex(a => new { a.ArtistId, a.AlbumName })
                .IsUnique();

            // Index for ReleaseDate 
            builder
                .HasIndex(a => a.ReleaseDate);

            // Cascading delete: if an Album is deleted, all related Tracks are deleted
            builder
                .HasMany(a => a.Tracks)
                .WithOne(t => t.Album)
                .HasForeignKey(t => t.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-Many relationship with Genre via AlbumGenre
            //builder
            //    .HasMany(a => a.Genres)
            //    .WithMany(g => g.Albums)
            ////    .UsingEntity("AlbumGenre");
            //builder.HasMany(a => a.Genres)
            //       .WithMany(g => g.Albums)
            //       .UsingEntity(j => j.ToTable("AlbumGenre"));

            //builder.HasData(
            //    new { AlbumsAlbumId = 1, GenresGenreId = 1 }, // Three Days Grace (album) - Post-Grunge
            //    new { AlbumsAlbumId = 1, GenresGenreId = 2 }, // Three Days Grace (album) - Alternative Metal
            //    new { AlbumsAlbumId = 2, GenresGenreId = 1 }, // One-X - Post-Grunge
            //    new { AlbumsAlbumId = 2, GenresGenreId = 2 }, // One-X - Alternative Metal
            //    new { AlbumsAlbumId = 3, GenresGenreId = 3 }, // Master of Puppets - Thrash Metal
            //    new { AlbumsAlbumId = 3, GenresGenreId = 4 }, // Master of Puppets - Heavy Metal
            //    new { AlbumsAlbumId = 4, GenresGenreId = 3 }, // Metallica - Thrash Metal
            //    new { AlbumsAlbumId = 4, GenresGenreId = 4 }, // Metallica - Heavy Metal
            //    new { AlbumsAlbumId = 5, GenresGenreId = 4 }, // Ten Thousand Fists - Heavy Metal
            //    new { AlbumsAlbumId = 5, GenresGenreId = 5 }, // Ten Thousand Fists - Nu Metal
            //    new { AlbumsAlbumId = 6, GenresGenreId = 4 }, // Immortalized - Heavy Metal
            //    new { AlbumsAlbumId = 6, GenresGenreId = 5 }  // Immortalized - Nu Metal
            //);

            builder.HasMany(a => a.Genres)
            .WithMany(g => g.Albums)
             .UsingEntity<Dictionary<string, object>>(
             "AlbumGenre",
            j => j.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
             j => j.HasOne<Album>().WithMany().HasForeignKey("AlbumId"),
             j =>
             {
                 j.HasKey("AlbumId", "GenreId"); // Явно вказуємо складений первинний ключ
                j.HasData(
                      new { AlbumId = 1, GenreId = 1 },
                       new { AlbumId = 1, GenreId = 2 },
                      new { AlbumId = 2, GenreId = 1 },
                      new { AlbumId = 2, GenreId = 2 },
                      new { AlbumId = 3, GenreId = 3 },
                      new { AlbumId = 3, GenreId = 4 },
                      new { AlbumId = 4, GenreId = 3 },
                      new { AlbumId = 4, GenreId = 4 },
                      new { AlbumId = 5, GenreId = 4 },
                      new { AlbumId = 5, GenreId = 5 },
                         new { AlbumId = 6, GenreId = 4 },
                      new { AlbumId = 6, GenreId = 5 }
                   );
             });

            // Albums

        }
    }
}