using System.ComponentModel.DataAnnotations;

namespace MyMusicCollection_API.Models
{
    public class GetAllArtistModel
    {
        public int ArtistId { get; set; }  
        public string bandName { get; set; }
        public string? Country { get; set; }
        public string yearsOfActivity { get; set; }
        public string? Biography { get; set; }

    }
}
