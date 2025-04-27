namespace MyMusicCollection_API.Models
{
    public class UpdateAlbumModel
    {
        public int AlbumId { get; set; } 
        public string AlbumName { get; set; }
        public int AlbumDuration { get; set; }
        public int TrackCount { get; set; }
        public string Label { get; set; }
        public string Format { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ArtistId { get; set; }
    }
}
