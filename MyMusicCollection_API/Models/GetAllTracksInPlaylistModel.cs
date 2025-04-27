namespace MyMusicCollection_API.Models
{
    public class GetAllTracksInPlaylistModel
    {
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public int Duration { get; set; }
        public int NumberInList { get; set; }
        public string LyricsAuthor { get; set; }
        public string MusicAuthor { get; set; }
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public int PlayListId { get; set; }
    }
}
