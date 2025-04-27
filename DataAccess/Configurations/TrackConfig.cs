using System.Reflection.Emit;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    internal class TrackConfig : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {

            builder.HasMany(t => t.Genres)
           .WithMany(g => g.Tracks)
           .UsingEntity<Dictionary<string, object>>(
               "TrackGenre",
               j => j.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
               j => j.HasOne<Track>().WithMany().HasForeignKey("TrackId"),
               j =>
               {
                   j.HasKey("TrackId", "GenreId"); // Явно вказуємо первинний ключ
               });

            // Traks
            builder.HasData(
                // Three Days Grace - Three Days Grace
                new Track
                {
                    TrackId = 1,
                    TrackName = "Burn",
                    Duration = 258, // 4:18
                    NumberInList = 1,
                    LyricsAuthor = "Adam Gontier",
                    MusicAuthor = "Three Days Grace",
                    AlbumId = 1
                },
                new Track
                {
                    TrackId = 2,
                    TrackName = "Just Like You",
                    Duration = 192, // 3:12
                    NumberInList = 2,
                    LyricsAuthor = "Adam Gontier",
                    MusicAuthor = "Three Days Grace",
                    AlbumId = 1
                },
                new Track
                {
                    TrackId = 3,
                    TrackName = "I Hate Everything About You",
                    Duration = 229, // 3:49
                    NumberInList = 3,
                    LyricsAuthor = "Adam Gontier",
                    MusicAuthor = "Three Days Grace",
                    AlbumId = 1
                },
                new Track
                {
                    TrackId = 4,
                    TrackName = "Home",
                    Duration = 255, // 4:15
                    NumberInList = 4,
                    LyricsAuthor = "Adam Gontier",
                    MusicAuthor = "Three Days Grace",
                    AlbumId = 1
                },
                // Three Days Grace - One-X
                new Track
                {
                    TrackId = 5,
                    TrackName = "Animal I Have Become",
                    Duration = 231, // 3:51
                    NumberInList = 1,
                    LyricsAuthor = "Adam Gontier",
                    MusicAuthor = "Three Days Grace",
                    AlbumId = 2
                },
                new Track
                {
                    TrackId = 6,
                    TrackName = "Pain",
                    Duration = 209, // 3:29
                    NumberInList = 2,
                    LyricsAuthor = "Adam Gontier",
                    MusicAuthor = "Three Days Grace",
                    AlbumId = 2
                },
                new Track
                {
                    TrackId = 7,
                    TrackName = "Never Too Late",
                    Duration = 208, // 3:28
                    NumberInList = 3,
                    LyricsAuthor = "Adam Gontier",
                    MusicAuthor = "Three Days Grace",
                    AlbumId = 2
                },
                new Track
                {
                    TrackId = 8,
                    TrackName = "Riot",
                    Duration = 207, // 3:27
                    NumberInList = 4,
                    LyricsAuthor = "Adam Gontier",
                    MusicAuthor = "Three Days Grace",
                    AlbumId = 2
                },
                // Metallica - Master of Puppets
                new Track
                {
                    TrackId = 9,
                    TrackName = "Battery",
                    Duration = 312, // 5:12
                    NumberInList = 1,
                    LyricsAuthor = "James Hetfield",
                    MusicAuthor = "Metallica",
                    AlbumId = 3
                },
                new Track
                {
                    TrackId = 10,
                    TrackName = "Master of Puppets",
                    Duration = 515, // 8:35
                    NumberInList = 2,
                    LyricsAuthor = "James Hetfield",
                    MusicAuthor = "Metallica",
                    AlbumId = 3
                },
                new Track
                {
                    TrackId = 11,
                    TrackName = "Welcome Home (Sanitarium)",
                    Duration = 387, // 6:27
                    NumberInList = 3,
                    LyricsAuthor = "James Hetfield",
                    MusicAuthor = "Metallica",
                    AlbumId = 3
                },
                new Track
                {
                    TrackId = 12,
                    TrackName = "Disposable Heroes",
                    Duration = 496, // 8:16
                    NumberInList = 4,
                    LyricsAuthor = "James Hetfield",
                    MusicAuthor = "Metallica",
                    AlbumId = 3
                },
                // Metallica - Metallica (The Black Album)
                new Track
                {
                    TrackId = 13,
                    TrackName = "Enter Sandman",
                    Duration = 331, // 5:31
                    NumberInList = 1,
                    LyricsAuthor = "James Hetfield",
                    MusicAuthor = "Metallica",
                    AlbumId = 4
                },
                new Track
                {
                    TrackId = 14,
                    TrackName = "Sad But True",
                    Duration = 324, // 5:24
                    NumberInList = 2,
                    LyricsAuthor = "James Hetfield",
                    MusicAuthor = "Metallica",
                    AlbumId = 4
                },
                new Track
                {
                    TrackId = 15,
                    TrackName = "The Unforgiven",
                    Duration = 386, // 6:26
                    NumberInList = 3,
                    LyricsAuthor = "James Hetfield",
                    MusicAuthor = "Metallica",
                    AlbumId = 4
                },
                new Track
                {
                    TrackId = 16,
                    TrackName = "Nothing Else Matters",
                    Duration = 388, // 6:28
                    NumberInList = 4,
                    LyricsAuthor = "James Hetfield",
                    MusicAuthor = "Metallica",
                    AlbumId = 4
                },
                // Disturbed - Ten Thousand Fists
                new Track
                {
                    TrackId = 17,
                    TrackName = "Ten Thousand Fists",
                    Duration = 214, // 3:34
                    NumberInList = 1,
                    LyricsAuthor = "David Draiman",
                    MusicAuthor = "Disturbed",
                    AlbumId = 5
                },
                new Track
                {
                    TrackId = 18,
                    TrackName = "Stricken",
                    Duration = 245, // 4:05
                    NumberInList = 2,
                    LyricsAuthor = "David Draiman",
                    MusicAuthor = "Disturbed",
                    AlbumId = 5
                },
                new Track
                {
                    TrackId = 19,
                    TrackName = "I'm Alive",
                    Duration = 283, // 4:43
                    NumberInList = 3,
                    LyricsAuthor = "David Draiman",
                    MusicAuthor = "Disturbed",
                    AlbumId = 5
                },
                new Track
                {
                    TrackId = 20,
                    TrackName = "Land of Confusion",
                    Duration = 287, // 4:47
                    NumberInList = 4,
                    LyricsAuthor = "David Draiman",
                    MusicAuthor = "Disturbed",
                    AlbumId = 5
                },
                // Disturbed - Immortalized
                new Track
                {
                    TrackId = 21,
                    TrackName = "The Vengeful One",
                    Duration = 253, // 4:13
                    NumberInList = 1,
                    LyricsAuthor = "David Draiman",
                    MusicAuthor = "Disturbed",
                    AlbumId = 6
                },
                new Track
                {
                    TrackId = 22,
                    TrackName = "Immortalized",
                    Duration = 263, // 4:23
                    NumberInList = 2,
                    LyricsAuthor = "David Draiman",
                    MusicAuthor = "Disturbed",
                    AlbumId = 6
                },
                new Track
                {
                    TrackId = 23,
                    TrackName = "The Sound of Silence",
                    Duration = 245, // 4:05
                    NumberInList = 3,
                    LyricsAuthor = "David Draiman",
                    MusicAuthor = "Disturbed",
                    AlbumId = 6
                },
                new Track
                {
                    TrackId = 24,
                    TrackName = "Fire It Up",
                    Duration = 227, // 3:47
                    NumberInList = 4,
                    LyricsAuthor = "David Draiman",
                    MusicAuthor = "Disturbed",
                    AlbumId = 6
                }
            );

            builder.HasMany(t => t.Genres)
             .WithMany(g => g.Tracks)
             .UsingEntity<Dictionary<string, object>>(
              "TrackGenre",
              j => j.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
              j => j.HasOne<Track>().WithMany().HasForeignKey("TrackId"),
              j =>
              {
                  j.HasKey("TrackId", "GenreId");
                  j.HasData(
                       new { TrackId = 1, GenreId = 1 }, // Burn - Post-Grunge
                       new { TrackId = 1, GenreId = 2 }, // Burn - Alternative Metal
                       new { TrackId = 2, GenreId = 1 }, // Just Like You - Post-Grunge
                       new { TrackId = 2, GenreId = 2 }, // Just Like You - Alternative Metal
                       new { TrackId = 9, GenreId = 3 }, // Battery - Thrash Metal
                       new { TrackId = 9, GenreId = 4 }, // Battery - Heavy Metal
                       new { TrackId = 13, GenreId = 3 }, // Enter Sandman - Thrash Metal
                       new { TrackId = 13, GenreId = 4 }, // Enter Sandman - Heavy Metal
                       new { TrackId = 17,GenreId = 4 }, // Ten Thousand Fists - Heavy Metal
                       new { TrackId = 17, GenreId = 5 }, // Ten Thousand Fists - Nu Metal
                       new { TrackId = 23, GenreId = 4 }, // The Sound of Silence - Heavy Metal
                       new { TrackId = 23, GenreId = 5 }  // The Sound of Silence - Nu Metal
                  );
              });

        }
    }
}