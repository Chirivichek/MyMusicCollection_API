using System.Reflection.Emit;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    internal class PlayListConfig : IEntityTypeConfiguration<PlayList>
    {

        public void Configure(EntityTypeBuilder<PlayList> builder)
        {
            // Спочатку конфігуруємо властивості
            builder.Property(p => p.PlayListName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(p => new { p.UserId, p.PlayListName })
                   .IsUnique();

            builder.HasIndex(p => p.DateCreated);

            // Налаштування зв'язків
            builder.HasOne(p => p.User)
                   .WithMany(u => u.PlayLists)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Потім додаємо seed-дані
            builder.HasData(
                new PlayList
                {
                    PlayListId = 1,
                    PlayListName = "Metal Favorites",
                    DateCreated = new DateTime(2025, 4, 13), // Додано обов'язкове поле
                    UserId = 1
                }
            );

            // Додавання зв'язків many-to-many
            builder.HasMany(p => p.Tracks)
                   .WithMany(t => t.PlayLists)
                   .UsingEntity<Dictionary<string, object>>(
                       "PlayListTrack",
                       j => j.HasOne<Track>().WithMany().HasForeignKey("TrackId"),
                       j => j.HasOne<PlayList>().WithMany().HasForeignKey("PlayListId"),
                       j =>
                       {
                           j.HasKey("PlayListId", "TrackId");
                           j.HasData(
                               new { PlayListId = 1, TrackId = 5 },
                               new { PlayListId = 1, TrackId = 13 },
                               new { PlayListId = 1, TrackId = 23 }
                           );
                       });
        }
    }
}