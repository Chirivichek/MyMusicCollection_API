using System.ComponentModel.DataAnnotations;

namespace MyMusicCollection_API.Models
{
    public class CreateUserCollectionModel
    {
        public DateTime DateAdded { get; set; }
        public string Status { get; set; } 
        public int AlbumId { get; set; } 
        public int UserId { get; set; } 
    }
}
