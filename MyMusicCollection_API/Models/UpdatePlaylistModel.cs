namespace MyMusicCollection_API.Models
{
    public class UpdatePlaylistModel
    {
        public string PlayListName { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; } 
    }
}
