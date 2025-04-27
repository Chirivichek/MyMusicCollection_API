using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyMusicCollection_API.Models
{
    public class CreateRatingAndReviewModel
    {
        public int Rating { get; set; } 
        public string? Comment { get; set; }

    }
}
