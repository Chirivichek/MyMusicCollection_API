using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyMusicCollection_API.Models
{
    public class CreateRatingAndReviewModel
    {
        public int Rating { get; set; } 
        public string Comment { get; set; }
        public int UserId { get; set; } // Foreign Key
        public int AlbumId { get; set; } // Foreign Key
    }
}
