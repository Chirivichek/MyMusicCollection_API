namespace MyMusicCollection_API.Models
{
    public class UpdateRatingAndReviewModel
    {
        public int Rating { get; set; }  
        public string Comment { get; set; }  
        public DateTime Date { get; set; }  
        public int AlbumId { get; set; }  
        public int UserId { get; set; }  
    }
}
