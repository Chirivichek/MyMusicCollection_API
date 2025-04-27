namespace MyMusicCollection_API.Models
{
    public class CreatePlaylistModel
    {
        public string PlayListName { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }  
    }
}
