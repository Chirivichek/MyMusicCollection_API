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
        }
    }
}
