using System.ComponentModel.DataAnnotations;

namespace MyMusicCollection_API.Models
{
    public class UpdateTrackModel
    {
        public string TrackName { get; set; }
        public int Duration { get; set; }
        public int NumberInList { get; set; }
        public string LyricsAuthor { get; set; }
        public string MusicAuthor { get; set; }
    }
}
