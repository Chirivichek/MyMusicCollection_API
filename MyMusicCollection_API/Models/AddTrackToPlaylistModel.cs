using DataAccess.Entities;

namespace MyMusicCollection_API.Models
{
    public class AddTrackToPlaylistModel
    {
        public int PlayListId { get; set; }  
        public int TrackId { get; set; }
        public ICollection<string>? Tracks { get; set; }  
    }
}
