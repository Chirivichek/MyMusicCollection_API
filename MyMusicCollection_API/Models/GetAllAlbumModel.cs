using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyMusicCollection_API.Models
{
    public class GetAllAlbumModel
    {
        public int AlbumId { get; set; } 
        public string AlbumName { get; set; }
        public int AlbumDuration { get; set; }
        public int TrackCount { get; set; }
        public string Label { get; set; }
        public string Format { get; set; } 
        public DateTime ReleaseDate { get; set; }
        public string bandName { get; set; }
        public ICollection<string>? Genres { get; set; } 
        public ICollection<string>? Tracks { get; set; } 
        public ICollection<int>? RatingsAndReviews { get; set; }
    }
}
