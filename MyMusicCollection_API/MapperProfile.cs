using AutoMapper;
using DataAccess.Entities;
using MyMusicCollection_API.Models;

namespace MyMusicCollection_API
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<CreateAlbumModel, Album>();
            CreateMap<UpdateAlbumModel, Album>();
            CreateMap<Album, GetAllAlbumModel>()
           .ForPath(dest => dest.bandName, opt => opt.MapFrom(src => src.Artist != null ? src.Artist.bandName : "Unknown Artist"))
           .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres != null ? src.Genres.Select(g => g.GenreName).ToList() : new List<string>())) 
           .ForMember(dest => dest.RatingsAndReviews, opt => opt.MapFrom(src => src.RatingsAndReviews != null ? src.RatingsAndReviews.Select(r => r.Rating).ToList() : new List<int>()));

            CreateMap<Genre, string>()
            .ConvertUsing(g => g.GenreName);

            CreateMap<RatingAndReview, int>()
            .ConvertUsing(r => r.Rating);  

            CreateMap<UpdateTrackModel, Track>();
            CreateMap<Track, GetAllTracksInPlaylistModel>();
            CreateMap<Track, GetAllTrackModel>()
            .ForMember(dest => dest.AlbumName, opt => opt.MapFrom(src => src.Album != null ? src.Album.AlbumName : "Unknown Album"));
            CreateMap<Artist, GetAllArtistModel>();
            CreateMap<CreateArtistModel, Artist>();
            CreateMap<UpdateArtistModel, Artist>();

            CreateMap<Genre,  GetAllGenreModel>();
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<UpdateGenreModel, Genre>();

            CreateMap<PlayList, GetAllPlaylistModel>();
            
            CreateMap<CreatePlaylistModel, PlayList>();
            CreateMap<UpdatePlaylistModel, PlayList>();
            CreateMap<AddTrackToPlaylistModel, Track>();

            CreateMap<User, GetAllUserModel>()
                .ForPath(dest => dest.PlayLists, opt => opt.MapFrom(src => src.PlayLists != null ? src.PlayLists.Select(p => p.PlayListName).ToList() : new List<string>())) 
                .ForMember(dest => dest.UserCollections, opt => opt.MapFrom(src => src.UserCollections != null ? src.UserCollections.Select(uc => uc.Album.AlbumName).ToList() : new List<string>()))
                .ForMember(dest => dest.RatingsAndReviews, opt => opt.MapFrom(src => src.RatingsAndReviews != null ? src.RatingsAndReviews.Select(rr => rr.Rating).ToList() : new List<int>()));;
            CreateMap<CreateUserModel, User>();
            CreateMap<UpdateUserModel, User>();

            CreateMap<RatingAndReview, GetAllRatingAndReviewModel>();
            CreateMap<CreateRatingAndReviewModel, RatingAndReview>();
            CreateMap<UpdateRatingAndReviewModel, RatingAndReview>();

            CreateMap<UserCollection, GetAllUserCollectionModel>()
            .ForMember(dest => dest.AlbumName, opt => opt.MapFrom(src => src.Album != null ? src.Album.AlbumName : "Unknown Album"));

            CreateMap<CreateUserCollectionModel, UserCollection>();
            CreateMap<UpdateUserCollectionModel, UserCollection>();
        }
    }
}
