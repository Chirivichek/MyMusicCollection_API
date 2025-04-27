using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Reflection;
using DataAccess.Entities;

namespace DataAccess.Data
{
    public class MusicCollectionBDcontext : DbContext
    {
        public MusicCollectionBDcontext()
        {
        }

        public MusicCollectionBDcontext(DbContextOptions options) : base(options)
        {
        }

        // entities : album, track, artist, userCollection, ratingsAndReviews, Playlists
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<RatingAndReview> RatingsAndReviews { get; set; }
        public DbSet<PlayList> PlayLists { get; set; }
        public DbSet<UserCollection> UserCollections { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                var cs = ConfigurationManager.ConnectionStrings["MusicCollectionDb"].ConnectionString;
                optionsBuilder.UseSqlServer(cs);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use Fluent API configurations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());        
           
        }
    }
}
