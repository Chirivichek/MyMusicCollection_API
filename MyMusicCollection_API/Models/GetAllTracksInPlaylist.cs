namespace MyMusicCollection_API.Models
{
    public class GetAllTracksInPlaylist
    {
        public int PlayListId { get; set; } 
        public string PlayListName { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }

        public ICollection<string>? Tracks { get; set; } 
    }
}
