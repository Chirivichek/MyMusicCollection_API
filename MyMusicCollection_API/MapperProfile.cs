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
           .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres != null ? src.Genres.Select(g => g.GenreName).ToList() : new List<string>())) // Змінено src.Artist.Genres на src.Genres
           .ForMember(dest => dest.RatingsAndReviews, opt => opt.MapFrom(src => src.RatingsAndReviews != null ? src.RatingsAndReviews.Select(r => r.Rating).ToList() : new List<int>()));

            CreateMap<Genre, string>()
            .ConvertUsing(g => g.GenreName);

            CreateMap<RatingAndReview, int>()
            .ConvertUsing(r => r.Rating);

           

            CreateMap<UpdateTrackModel, Track>();
            CreateMap<Track, GetAllTracksInPlaylist>();
            CreateMap<Track, GetAllTrack>()
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
            
            
        }
    }
}
