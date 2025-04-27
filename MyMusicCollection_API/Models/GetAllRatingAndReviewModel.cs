using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyMusicCollection_API.Models
{
    public class GetAllRatingAndReviewModel
    {
        public int RatingAndReviewId { get; set; }  
        public int Rating { get; set; }  
        public string Comment { get; set; }
        public int UserId { get; set; } 
        public int AlbumId { get; set; } 
    }
}
