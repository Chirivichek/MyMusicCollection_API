using System.ComponentModel.DataAnnotations;
using DataAccess.Entities;

namespace MyMusicCollection_API.Models
{
    public class GetAllUserCollectionModel
    {
        public int UserCollectionId { get; set; }
        public DateTime DateAdded { get; set; }
        public string Status { get; set; }  
        public int AlbumId { get; set; } 
        public int UserId { get; set; }
        public string AlbumName { get; set; }
    }
}
