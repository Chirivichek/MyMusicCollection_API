using DataAccess.Entities;

namespace MyMusicCollection_API.Models
{
    public class GetAllUserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<string>? UserCollections { get; set; }
        public ICollection<string>? PlayLists { get; set; }
        public ICollection<string>? RatingsAndReviews { get; set; }
    }
}
